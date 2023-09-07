public static class ConqForceValues
{
    //NA
    public static string Neutral = "#FFFFFF";
    public static string StronDrium = "#1399D2";
    public static string RepubofCop = "#FF2626";

    public static string ConqToColor(ConqForce cf)
    {
        switch(cf)
        {
            case ConqForce.NA:
                return Neutral;
            case ConqForce.SD:
                return StronDrium;
            case ConqForce.RC:
                return RepubofCop;
            default:
                return "Im going to throw an error";
        }
    }
}
