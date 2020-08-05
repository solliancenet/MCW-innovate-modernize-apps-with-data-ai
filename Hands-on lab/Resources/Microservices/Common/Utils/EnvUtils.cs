using System;

namespace Common.Utils
{
    public static class EnvUtils
    {
        public static string Get(string environmentVariableName)
        {
            var value = Environment.GetEnvironmentVariable(environmentVariableName);

            if (value == null)
            {
                throw new Exception($"Could not find Environment Variable {environmentVariableName}.");
            }

            return value;
        }
    }
}