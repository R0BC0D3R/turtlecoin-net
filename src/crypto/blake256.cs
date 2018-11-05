/*
 * The blake256_* and blake224_* functions are largely copied from
 * blake256_light.c and blake224_light.c from the BLAKE website:
 *
 *     http://131002.net/blake/
 *
 * The hmac_* functions implement HMAC-BLAKE-256 and HMAC-BLAKE-224.
 * HMAC is specified by RFC 2104.
 */



public class state
{
  public uint32_t[] h = Arrays.InitializeWithDefaultInstances<uint32_t>(8);
  public uint32_t[] s = Arrays.InitializeWithDefaultInstances<uint32_t>(4);
  public uint32_t[] t = Arrays.InitializeWithDefaultInstances<uint32_t>(2);
  public int buflen;
  public int nullt;
  public uint8_t[] buf = Arrays.InitializeWithDefaultInstances<uint8_t>(64);
}

public class hmac_state
{
  public state inner = new state();
  public state outer = new state();
}