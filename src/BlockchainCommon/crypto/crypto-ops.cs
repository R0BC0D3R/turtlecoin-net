// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


/* From fe.h */


/* From ge.h */

public class ge_p2
{
  public int[] X = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] Y = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] Z = Arrays.InitializeWithDefaultInstances<int>(10);
}

public class ge_p3
{
  public int[] X = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] Y = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] Z = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] T = Arrays.InitializeWithDefaultInstances<int>(10);
}

public class ge_p1p1
{
  public int[] X = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] Y = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] Z = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] T = Arrays.InitializeWithDefaultInstances<int>(10);
}

public class ge_precomp
{
  public int[] yplusx = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] yminusx = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] xy2d = Arrays.InitializeWithDefaultInstances<int>(10);
}

public class ge_cached
{
  public int[] YplusX = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] YminusX = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] Z = Arrays.InitializeWithDefaultInstances<int>(10);
  public int[] T2d = Arrays.InitializeWithDefaultInstances<int>(10);
}