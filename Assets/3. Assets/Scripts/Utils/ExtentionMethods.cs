using System;

public static class ExtentionMethods
{
    public static bool IsSet(this Enum input, Enum matchTo)
    {
        return (Convert.ToUInt32(input) & Convert.ToUInt32(matchTo)) != 0;
    }
}
