// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


/* From fe.h */


/* From ge.h */

public class ge_p2
{
  public int32_t[] X = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] Y = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] Z = Arrays.InitializeWithDefaultInstances<int32_t>(10);
}

public class ge_p3
{
  public int32_t[] X = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] Y = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] Z = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] T = Arrays.InitializeWithDefaultInstances<int32_t>(10);
}

public class ge_p1p1
{
  public int32_t[] X = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] Y = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] Z = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] T = Arrays.InitializeWithDefaultInstances<int32_t>(10);
}

public class ge_precomp
{
  public int32_t[] yplusx = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] yminusx = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] xy2d = Arrays.InitializeWithDefaultInstances<int32_t>(10);
}

public class ge_cached
{
  public int32_t[] YplusX = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] YminusX = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] Z = Arrays.InitializeWithDefaultInstances<int32_t>(10);
  public int32_t[] T2d = Arrays.InitializeWithDefaultInstances<int32_t>(10);
}