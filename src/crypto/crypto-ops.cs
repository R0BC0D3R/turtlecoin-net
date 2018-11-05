// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



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