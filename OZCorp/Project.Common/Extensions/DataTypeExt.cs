namespace Project.Common.Extensions
{
    public static class DataTypeExt
    {
        public static string ToStringBoolean(this bool value)
            => value ? "Yes" : "No";
        public static string ToStringBoolean(this bool? value)
            => value?.ToStringBoolean();

        public static string ToPercentage(this decimal value)
            => string.Format("{0:P2}", value);
        public static string ToPercentage(this decimal? value)
            => value?.ToPercentage();

        public static string ToMoney(this decimal value)
            => string.Format("{0:N2}", value);
        public static string ToMoney(this decimal? value)
            => value?.ToMoney();

    }
}
