namespace Bebone.Text;

internal static class FreeTypeHelper
{
    public static int From26Dot6(int value)
    {
        return value >> 6;
    }
}
