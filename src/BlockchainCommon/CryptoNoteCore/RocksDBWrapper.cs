// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using CryptoNote;
using Logging;
using System;
using System.Collections.Generic;


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ROCKSDB_USING_THREAD_STATUS !ROCKSDB_LITE && !NROCKSDB_THREAD_STATUS && !OS_MACOSX && !IOS_CROSS_COMPILE



namespace CryptoNote
{

public class RocksDBWrapper : IDataBase
{
  public RocksDBWrapper(Logging.ILogger logger)
  {
	  this.logger = new Logging.LoggerRef(logger, "RocksDBWrapper");
	  this.state = State.NOT_INITIALIZED;

  }
  public override void Dispose()
  {

	  base.Dispose();
  }

//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  RocksDBWrapper(const RocksDBWrapper&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  RocksDBWrapper(RocksDBWrapper&&) = delete;

//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  RocksDBWrapper& operator =(const RocksDBWrapper&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  RocksDBWrapper& operator =(RocksDBWrapper&&) = delete;

  public void init(DataBaseConfig config)
  {
	if (state.load() != State.NOT_INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.ALREADY_INITIALIZED));
	}

	string dataDir = getDataDir(config);

	logger(INFO) << "Opening DB in " << dataDir;

	rocksdb.DB dbPtr;

	rocksdb.Options dbOptions = getDBOptions(config);
	rocksdb.Status status = rocksdb.DB.Open(dbOptions, dataDir, dbPtr);
	if (status.ok())
	{
	  logger(INFO) << "DB opened in " << dataDir;
	}
	else if (!status.ok() && status.IsInvalidArgument())
	{
	  logger(INFO) << "DB not found in " << dataDir << ". Creating new DB...";
	  dbOptions.create_if_missing = true;
	  rocksdb.Status status = rocksdb.DB.Open(dbOptions, dataDir, dbPtr);
	  if (!status.ok())
	  {
		logger(ERROR) << "DB Error. DB can't be created in " << dataDir << ". Error: " << status.ToString();
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.INTERNAL_ERROR));
	  }
	}
	else if (status.IsIOError())
	{
	  logger(ERROR) << "DB Error. DB can't be opened in " << dataDir << ". Error: " << status.ToString();
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.IO_ERROR));
	}
	else
	{
	  logger(ERROR) << "DB Error. DB can't be opened in " << dataDir << ". Error: " << status.ToString();
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.INTERNAL_ERROR));
	}

	db.reset(dbPtr);
	state.store(State.INITIALIZED);
  }
  public void shutdown()
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.NOT_INITIALIZED));
	}

	logger(INFO) << "Closing DB.";
	db.Flush(new rocksdb.FlushOptions());
	db.SyncWAL();
	db.reset();
	state.store(State.NOT_INITIALIZED);
  }
  public void destroy(DataBaseConfig config)
  {
	if (state.load() != State.NOT_INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.ALREADY_INITIALIZED));
	}

	string dataDir = getDataDir(config);

	logger(WARNING) << "Destroying DB in " << dataDir;

	rocksdb.Options dbOptions = getDBOptions(config);
	rocksdb.Status status = rocksdb.DestroyDB(dataDir, dbOptions);

	if (status.ok())
	{
	  logger(WARNING) << "DB destroyed in " << dataDir;
	}
	else
	{
	  logger(ERROR) << "DB Error. DB can't be destroyed in " << dataDir << ". Error: " << status.ToString();
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.INTERNAL_ERROR));
	}
  }

  public override std::error_code write(IWriteBatch batch)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.NOT_INITIALIZED));
	}

	return write(batch, false);
  }
  public override std::error_code writeSync(IWriteBatch batch)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.NOT_INITIALIZED));
	}

	return write(batch, true);
  }
  public override std::error_code read(IReadBatch batch)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw new System.Exception("Not initialized.");
	}

	rocksdb.ReadOptions readOptions = new rocksdb.ReadOptions();

	List<string> rawKeys = new List<string>(batch.getRawKeys());
	List<rocksdb.Slice> keySlices = new List<rocksdb.Slice>();
	keySlices.Capacity = rawKeys.Count;
	foreach (string key in rawKeys)
	{
	  keySlices.emplace_back(new rocksdb.Slice(key));
	}

	List<string> values = new List<string>();
	values.Capacity = rawKeys.Count;
	List<rocksdb.Status> statuses = db.MultiGet(readOptions, keySlices, values);

	std::error_code error = new std::error_code();
	List<bool> resultStates = new List<bool>();
	foreach (rocksdb  in :Status & status : statuses)
	{
	  if (!status.ok() && !status.IsNotFound())
	  {
		return GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.INTERNAL_ERROR);
	  }
	  resultStates.Add(status.ok());
	}

	batch.submitRawResult(values, resultStates);
	return std::error_code();
  }

  private std::error_code write(IWriteBatch batch, bool sync)
  {
	rocksdb.WriteOptions writeOptions = new rocksdb.WriteOptions();
	writeOptions.sync = sync;

	rocksdb.WriteBatch rocksdbBatch = new rocksdb.WriteBatch();
	List<Tuple<string, string>> rawData = new List<Tuple<string, string>>(batch.extractRawDataToInsert());
	foreach (Tuple<string, string> kvPair in rawData)
	{
	  rocksdbBatch.Put(new rocksdb.Slice(kvPair.Item1), new rocksdb.Slice(kvPair.Item2));
	}

	List<string> rawKeys = new List<string>(batch.extractRawKeysToRemove());
	foreach (string key in rawKeys)
	{
	  rocksdbBatch.Delete(new rocksdb.Slice(key));
	}

	rocksdb.Status status = db.Write(writeOptions, rocksdbBatch);

	if (!status.ok())
	{
	  logger(ERROR) << "Can't write to DB. " << status.ToString();
	  return GlobalMembers.make_error_code(CryptoNote.error.DataBaseErrorCodes.INTERNAL_ERROR);
	}
	else
	{
	  return std::error_code();
	}
  }

  private rocksdb.Options getDBOptions(DataBaseConfig config)
  {
	rocksdb.DBOptions dbOptions = new rocksdb.DBOptions();
	dbOptions.IncreaseParallelism(config.getBackgroundThreadsCount());
	dbOptions.info_log_level = rocksdb.InfoLogLevel.WARN_LEVEL;
	dbOptions.max_open_files = config.getMaxOpenFiles();

	rocksdb.ColumnFamilyOptions fOptions = new rocksdb.ColumnFamilyOptions();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: fOptions.write_buffer_size = static_cast<uint>(config.getWriteBufferSize());
	fOptions.write_buffer_size.CopyFrom((uint)config.getWriteBufferSize());
	// merge two memtables when flushing to L0
	fOptions.min_write_buffer_number_to_merge = 2;
	// this means we'll use 50% extra memory in the worst case, but will reduce
	// write stalls.
	fOptions.max_write_buffer_number = 6;
	// start flushing L0->L1 as soon as possible. each file on level0 is
	// (memtable_memory_budget / 2). This will flush level 0 when it's bigger than
	// memtable_memory_budget.
	fOptions.level0_file_num_compaction_trigger = 20;

	fOptions.level0_slowdown_writes_trigger = 30;
	fOptions.level0_stop_writes_trigger = 40;

	// doesn't really matter much, but we don't want to create too many files
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: fOptions.target_file_size_base = config.getWriteBufferSize() / 10;
	fOptions.target_file_size_base.CopyFrom(config.getWriteBufferSize() / 10);
	// make Level1 size equal to Level0 size, so that L0->L1 compactions are fast
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: fOptions.max_bytes_for_level_base = config.getWriteBufferSize();
	fOptions.max_bytes_for_level_base.CopyFrom(config.getWriteBufferSize());
	fOptions.num_levels = 10;
	fOptions.target_file_size_multiplier = 2;
	// level style compaction
	fOptions.compaction_style = rocksdb.CompactionStyle.kCompactionStyleLevel;

	fOptions.compression_per_level.Resize(fOptions.num_levels);
	for (int i = 0; i < fOptions.num_levels; ++i)
	{
	  fOptions.compression_per_level[i] = rocksdb.CompressionType.kNoCompression;
	}

	rocksdb.BlockBasedTableOptions tableOptions = new rocksdb.BlockBasedTableOptions();
	tableOptions.block_cache = rocksdb.NewLRUCache(config.getReadCacheSize());
	rocksdb.TableFactory tfp = new rocksdb.TableFactory(NewBlockBasedTableFactory(tableOptions));
	fOptions.table_factory = tfp;

	return new rocksdb.Options(dbOptions, fOptions);
  }
  private string getDataDir(DataBaseConfig config)
  {
	if (config.getTestnet())
	{
	  return config.getDataDir() + '/' + GlobalMembers.TESTNET_DB_NAME;
	}
	else
	{
	  return config.getDataDir() + '/' + GlobalMembers.DB_NAME;
	}
  }

  private enum State
  {
	NOT_INITIALIZED,
	INITIALIZED
  }

  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private std::unique_ptr<rocksdb.DB> db = new std::unique_ptr<rocksdb.DB>();
  private std::atomic<State> state = new std::atomic<State>();
}
}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace