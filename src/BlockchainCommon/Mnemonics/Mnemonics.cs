// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;
using System.Linq;

namespace Mnemonics
{
    public static class GlobalMembers
    {
        public static Dictionary<string, Crypto.SecretKey> MnemonicToPrivateKey(string words)
        {
            List<string> wordsList = new List<string>();

            /* Convert whitespace separated string into vector of words */
            //TODO: Verify that this was translated correctly
            wordsList = words.Split(' ').ToList();

            //std::istringstream stream = new std::istringstream(words);            
            //for (string s; stream >> s;)
            //{
            //    wordsList.Add(s);
            //}

            return MnemonicToPrivateKey(new List<string>(wordsList));
        }

        /* Note - if the returned string is not empty, it is an error message, and
		   the returned secret key is not initialized. */
        public static Dictionary<string, Crypto.SecretKey> MnemonicToPrivateKey(List<string> words)
        {
            Crypto.SecretKey key = new Crypto.SecretKey();

            int len = words.Count;

            /* Mnemonics must be 25 words long */
            if (len != 25)
            {
                string str;

                /* Write out "word" or "words" to make the grammar of the next sentence
				   correct, based on if we have 1 or more words */
                string wordPlural = len == 1 ? "word" : "words";

                str = "Mnemonic seed is wrong length - It should be 25 words long, but it is " + len + " " + wordPlural + " long!";

                return new Dictionary<string, Crypto.SecretKey>{{ str, key }};
            }

            /* All words must be present in the word list */
            foreach (string word in words)
            {
                if(!WordList.GlobalMembers.EnglishHash.Contains(word.ToLower()))
                {
                    string str = "Mnemonic seed has invalid word - " + word + " is not in the English word list!";

                    return new Dictionary<string, Crypto.SecretKey> { { str, key} };
                }
            }

            /* The checksum must be correct */
            if (!HasValidChecksum(new List<string>(words)))
            {
                return new Dictionary<string, Crypto.SecretKey> { { "Mnemonic seed has incorrect checksum!", key } };
            }

            List<int> wordIndexes = GetWordIndexes(new List<string>(words));

            List<ushort> data = new List<ushort>();

            for (int i = 0; i < words.Count - 1; i += 3)
            {
                /* Take the indexes of these three words in the word list */
                int w1 = wordIndexes[i];
                int w2 = wordIndexes[i + 1];
                int w3 = wordIndexes[i + 2];

                /* Word list length */
                int wlLen = WordList.GlobalMembers.EnglishList.Count;

                /* no idea what this does lol */
                ushort val = (ushort)(w1 + wlLen * (((wlLen - w1) + w2) % wlLen) + wlLen * wlLen * (((wlLen - w2) + w3) % wlLen));

                /* Don't know what this is testing either */
                if (!(val % wlLen == w1))
                {
                    return new Dictionary<string, Crypto.SecretKey> { { "Mnemonic seed is invalid!", key } };
                }

                /* Interpret val as 4 ushort's */
                //TODO: Need to revisit this. Original:
                //const auto ptr = reinterpret_cast <const uint8_t*> (&val);
                ///* Append to private key */
                //for (int j = 0; j < 4; j++)
                //{
                //    data.push_back(ptr[j]);
                //}

                short ptr = Convert.ToInt16(val);

                /* Append to private key */
                for (int j = 0; j < 4; j++)
                {
                    key.data[j] = val;
                }
            }

            return new Dictionary<string, Crypto.SecretKey> { { string.Empty, key } };
        }

        public static string PrivateKeyToMnemonic(Crypto.SecretKey privateKey)
        {
            List<string> words = new List<string>();

            for (int i = 0; i < 32 - 1; i += 4)
            {
                /* Read the array as a uint array */
                int ptr = (int)privateKey.data[i];

                /* Take the first element of the array (since we have already done the offset */
                //int val = ptr[0];
                int val = ptr;

                int wlLen = WordList.GlobalMembers.EnglishList.Count;

                int w1 = val % wlLen;
                int w2 = ((val / wlLen) + w1) % wlLen;
                int w3 = (((val / wlLen) / wlLen) + w2) % wlLen;

                words.Add(WordList.GlobalMembers.EnglishList[w1]);
                words.Add(WordList.GlobalMembers.EnglishList[w2]);
                words.Add(WordList.GlobalMembers.EnglishList[w3]);
            }

            words.Add(GetChecksumWord(new List<string>(words)));

            string result = string.Empty;

            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    result += " ";
                }

                result += word;
            }

            return result;
        }

        /* Assumes the input is 25 words long */
        public static bool HasValidChecksum(List<string> words)
        {
            /* Make a copy since erase() is mutating */
            List<string> wordsNoChecksum = words.ToList();

            /* Remove the last checksum word */
            //TODO: Make sure this was translated correctly
            wordsNoChecksum.RemoveAt(wordsNoChecksum.Count - 1);

            /* Assert the last word (the checksum word) is equal to the derived
			   checksum */
            return words.Last() == GetChecksumWord(new List<string>(wordsNoChecksum));
        }

        public static string GetChecksumWord(List<string> words)
        {
            string trimmed = string.Empty;

            /* Take the first 3 char from each of the 24 words */
            foreach (string word in words)
            {
                trimmed += word.Substring(0, 3);
            }

            /* Hash the data */
            //TODO: Changed ulong to int. Make sure this works
            int hash = Convert.ToInt32(CRC32.GlobalMembers.Crc32(trimmed));

            /* Modulus the hash by the word length to get the index of the checksum word */
            return words[hash % words.Count];
        }

        public static List<int> GetWordIndexes(List<string> words)
        {
            List<int> result = new List<int>();

            foreach (var word in words)
            {
                /* Find the iterator of our word in the wordlist */
                //const auto it = std::find(WordList::English.begin(), WordList::English.end(), word);
                int it = WordList.GlobalMembers.EnglishList.IndexOf(word);

                /* Take it away from the beginning of the vector, giving us the index of the item in the vector */
                //result.push_back(static_cast<int>(std::distance(WordList::English.begin(), it)));
                //TODO: Not sure if this will work
                result.Add(WordList.GlobalMembers.EnglishList.Count - it);
            }

            return result;
        }
    }
}