using System.Data;
using Dapper;
using Newtonsoft.Json.Linq;


namespace MXServer_API.DB.Implementation
{
    public class JObjectTypeHandler : SqlMapper.TypeHandler<JObject>
    {
        public override JObject Parse(object value)
        {
            var json = value.ToString();
            if (string.IsNullOrEmpty(json)) { return null; }
            return JObject.Parse(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, JObject value)
        {
            parameter.Value = value.ToString();
        }
    }

    public class JArrayTypeHandler : SqlMapper.TypeHandler<JArray>
    {
        public override JArray Parse(object value)
        {
            var json = value.ToString();
            if (string.IsNullOrEmpty(json)) { return null; }
            return JArray.Parse(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, JArray value)
        {
            parameter.Value = value.ToString();
        }
    }
}
