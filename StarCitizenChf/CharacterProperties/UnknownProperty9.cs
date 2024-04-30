using System.Runtime.InteropServices;

namespace StarCitizenChf;

internal sealed class UnknownProperty9
{
    public const uint Key = 0x_b3_21_bc_4f;
    public const string KeyRep = "4F-BC-21-B3";
    //same as above, 3 constant ints and then the weird 0-19 thing

    public static UnknownProperty9 Read(ref SpanReader reader)
    {
        return new UnknownProperty9();
    }
}
