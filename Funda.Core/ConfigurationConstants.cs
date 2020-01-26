namespace Funda.Core
{
    public static class ConfigurationConstants
    {
        public const string BaseUri = @"http://partnerapi.funda.nl";
        public const string ObjectsJsonPath = @"feeds/Aanbod.svc/JSON";
        public const string ForSaleAndSearchRequest = @"?type=koop&zo=";
        public const string Garden = "tuin";
        public const string ElementsIteration = "&page={0}&pagesize=25";
        public const string AccessKey = "ac1b0b1572524640a0ecc54de453ea9f";
        public const string LocationKey = "amsterdam";
        public const string EstateAgentId = "MakelaarId";
        public const string EstateAgentName = "MakelaarNaam";
        public const string EstateObjects = "Objects";
        public const string EstateElementId = "Id";
        public const string RequestLimitExceededError = "Request limit exceeded";
    }
}