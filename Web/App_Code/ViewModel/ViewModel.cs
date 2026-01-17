using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZWL.Common;
using ZWL.DBUtility;

/// <summary>
/// ViewModel 的摘要说明
/// </summary>
public class ViewModel
{
    public ViewModel()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
}

public class HolidayResult
{
    public string code { get; set; }
    public int data { get; set; }
}

public class JsonResult
{
    private bool _code;
    private object _data;
    private string _message;
    public bool Code
    {
        get
        {
            return _code;
        }
        set
        {
            value = _code;
        }
    }
    public object Data
    {
        get
        {
            return _data;
        }
        set
        {
            value = _data;
        }
    }
    public string Message
    {
        get
        {
            return _message;
        }
        set
        {
            value = _message;
        }
    }
    public JsonResult(bool code, string msg)
    {
        _code = code;
        _message = msg;
    }
    public JsonResult(bool code, string msg, object data)
    {
        _code = code;
        _message = msg;
        _data = data;
    }
}
public class ConnectedUser
{
    public string ConnectionId { get; set; }
    public string UserName { get; set; }
    public long SignInTime { get; set; }

}
public enum AuthorizeType
{
    Token,
}
public enum HttpVerb
{
    Unparsed,
    Unknown,
    GET,
    PUT,
    HEAD,
    POST,
    DEBUG,
    DELETE
}

public enum Carrier { Unknown, ChinaMobile, ChinaUnicom, ChinaTelecom }
#region 综合信息统计中，圆环图数据的实体类
public class resultmsg
{
    public string status { get; set; }
    public string errMessage { get; set; }
    public string rows { get; set; }
}
public class StatisticsInfo
{
    public string elementID { get; set; }
    public string categroy { get; set; }
    public string value { get; set; }
    public List<PieInfo> pieInfos { get; set; }
}

public class PieInfo
{
    public string type { get; set; }
    public string total { get; set; }
    public List<PieInfoMingXi> PieInfoMingXiList { get; set; }
}

public class PieInfoMingXi
{
    public string name { get; set; }
    public string value { get; set; }

    /// <summary>
    /// 获得多个数据实体
    /// </summary>
    public List<PieInfoMingXi> GetModelList(string strSql)
    {
        var ds = DbHelperSQL.Query(strSql);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            var dt = ds.Tables[0];
            return DataTableHelper.ConvertTo<PieInfoMingXi>(dt);
        }
        return new List<PieInfoMingXi>();
    }
}

public class WebLocationResult
{
    public int status { get; set; }
    public LocationResult result { get; set; }
    public string msg { get; set; }
}
public class LocationResult
{
    public PointResult location { get; set; }
    public int precise { get; set; }
    public int confidence { get; set; }
    public int comprehension { get; set; }
    public string level { get; set; }
}
public class PointResult
{
    public decimal lng { get; set; }
    public decimal lat { get; set; }
}
#endregion

public class TKeyValuePair<TKey, TValue>
{
    private TKey key;

    private TValue value;

    public TKey Key
    {

        get
        {
            return this.key;
        }
        set
        {
            this.key = value;
        }
    }


    public TValue Value
    {

        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
        }
    }

    public TKeyValuePair()
    {
    }

    public TKeyValuePair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }


    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append('[');
        if (this.Key != null)
        {
            StringBuilder arg_33_0 = stringBuilder;
            TKey tKey = this.Key;
            arg_33_0.Append(tKey.ToString());
        }
        stringBuilder.Append(", ");
        if (this.Value != null)
        {
            StringBuilder arg_67_0 = stringBuilder;
            TValue tValue = this.Value;
            arg_67_0.Append(tValue.ToString());
        }
        stringBuilder.Append(']');
        return stringBuilder.ToString();
    }
}
namespace ViewModels
{
    public class SysColumnInfo
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public string Desc { get; set; }
    }

    public class cdclass
    {
        public decimal submitAmt;
        public decimal fukuanAmt;
        public decimal circleAmt;
        public decimal unfukuanAmt;
    }
    public class ysfclass
    {
        public decimal yiSJE;
        public decimal yingSJE;
        public decimal yiFJE;
        public decimal yingFJE;
        public decimal jieSJE;
    }

    public class AppVersionInfoOutput
    {
        /// <summary>
        /// 版本更新内容 支持<br>自动换行
        ///describe: '1. 修复已知问题<br>
        ///2. 优化用户体验', 
        ///edition_url: '', //apk、wgt包下载地址或者应用市场地址  安卓应用市场 market://details?id=xxxx 苹果store itms-apps://itunes.apple.com/cn/app/xxxxxx
        ///edition_force: 0, //是否强制更新 0代表否 1代表是
        ///package_type: 1, //0是整包升级（apk或者appstore或者安卓应用市场） 1是wgt升级
        ///edition_issue:1, //是否发行  0否 1是 为了控制上架应用市场审核时不能弹出热更新框
        ///edition_number:100, //版本号 最重要的manifest里的版本号 （检查更新主要以服务器返回的edition_number版本号是否大于当前app的版本号来实现是否更新）
        ///edition_name:'1.0.0',// 版本名称 manifest里的版本名称
        ///edition_silence:0, // 是否静默更新 0代表否 1代表是
        /// </summary>
        public string appname { get; set; }
        public string describe { get; set; }
        public string edition_url { get; set; }
        public int edition_force { get; set; }
        public int package_type { get; set; }
        public int edition_issue { get; set; }
        public int edition_number { get; set; }
        public int edition_silence { get; set; }
        public string edition_name { get; set; }
    }

    public class LoginInput
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
    public class ApiBaoCanInput
    {

    }
}
namespace ViewModels.KitchenSink
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class StandardShape
    {
        [JsonProperty("cells")]
        public List<Cell> Cells { get; set; }
    }

    public partial class Cell
    {
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public VertexClass Position { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public Size Size { get; set; }

        [JsonProperty("angle", NullValueHandling = NullValueHandling.Ignore)]
        public long? Angle { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("z")]
        public long Z { get; set; }

        [JsonProperty("attrs")]
        public CellAttrs Attrs { get; set; }

        [JsonProperty("router", NullValueHandling = NullValueHandling.Ignore)]
        public Connector Router { get; set; }

        [JsonProperty("connector", NullValueHandling = NullValueHandling.Ignore)]
        public Connector Connector { get; set; }

        [JsonProperty("labels", NullValueHandling = NullValueHandling.Ignore)]
        public List<Label> Labels { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public Source Source { get; set; }

        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public Source Target { get; set; }

        [JsonProperty("vertices", NullValueHandling = NullValueHandling.Ignore)]
        public List<VertexClass> Vertices { get; set; }
        public string Link { get; set; }
    }

    public partial class CellAttrs
    {
        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public Body Body { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public HeaderText Label { get; set; }

        [JsonProperty("root", NullValueHandling = NullValueHandling.Ignore)]
        public Root Root { get; set; }

        [JsonProperty("line", NullValueHandling = NullValueHandling.Ignore)]
        public Line Line { get; set; }

        [JsonProperty("header", NullValueHandling = NullValueHandling.Ignore)]
        public Header Header { get; set; }

        [JsonProperty("headerText", NullValueHandling = NullValueHandling.Ignore)]
        public HeaderText HeaderText { get; set; }

        [JsonProperty("bodyText", NullValueHandling = NullValueHandling.Ignore)]
        public BodyText BodyText { get; set; }
    }

    public partial class Body
    {
        [JsonProperty("refPoints", NullValueHandling = NullValueHandling.Ignore)]
        public string RefPoints { get; set; }

        [JsonProperty("strokeWidth", NullValueHandling = NullValueHandling.Ignore)]
        public long? StrokeWidth { get; set; }

        [JsonProperty("stroke")]
        public Stroke Stroke { get; set; }

        [JsonProperty("fill")]
        public BodyFill Fill { get; set; }

        [JsonProperty("strokeDasharray")]
        public string StrokeDasharray { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("rx", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rx { get; set; }

        [JsonProperty("ry", NullValueHandling = NullValueHandling.Ignore)]
        public long? Ry { get; set; }
    }

    public partial class BodyText
    {
        [JsonProperty("refY2")]
        public long RefY2 { get; set; }

        [JsonProperty("fontSize")]
        public long FontSize { get; set; }

        [JsonProperty("fill")]
        public BodyFill Fill { get; set; }

        [JsonProperty("textWrap")]
        public TextWrap TextWrap { get; set; }

        [JsonProperty("fontFamily")]
        public FontFamily FontFamily { get; set; }

        [JsonProperty("fontWeight")]
        public FontWeight FontWeight { get; set; }

        [JsonProperty("strokeWidth")]
        public long StrokeWidth { get; set; }
    }

    public partial class TextWrap
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("ellipsis")]
        public bool Ellipsis { get; set; }
    }

    public partial class Header
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("strokeWidth")]
        public long StrokeWidth { get; set; }

        [JsonProperty("stroke")]
        public Stroke Stroke { get; set; }

        [JsonProperty("fill")]
        public BodyFill Fill { get; set; }

        [JsonProperty("strokeDasharray")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StrokeDasharray { get; set; }
    }

    public partial class HeaderText
    {
        [JsonProperty("refY", NullValueHandling = NullValueHandling.Ignore)]
        public long? RefY { get; set; }

        [JsonProperty("fontSize")]
        public long FontSize { get; set; }

        [JsonProperty("fill")]
        public HeaderTextFill Fill { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fontFamily")]
        public FontFamily FontFamily { get; set; }

        [JsonProperty("fontWeight")]
        public FontWeight FontWeight { get; set; }

        [JsonProperty("strokeWidth")]
        public long StrokeWidth { get; set; }
    }

    public partial class Line
    {
        [JsonProperty("stroke", NullValueHandling = NullValueHandling.Ignore)]
        public string Stroke { get; set; }

        [JsonProperty("strokeWidth")]
        public long StrokeWidth { get; set; }

        [JsonProperty("targetMarker", NullValueHandling = NullValueHandling.Ignore)]
        public TargetMarker TargetMarker { get; set; }
    }

    public partial class TargetMarker
    {
        [JsonProperty("d")]
        public D D { get; set; }
    }

    public partial class Root
    {
        [JsonProperty("dataTooltipPosition")]
        public DataTooltipPosition DataTooltipPosition { get; set; }

        [JsonProperty("dataTooltipPositionSelector")]
        public DataTooltipPositionSelector DataTooltipPositionSelector { get; set; }
    }

    public partial class Connector
    {
        [JsonProperty("name")]
        public ConnectorName Name { get; set; }
    }

    public partial class Label
    {
        [JsonProperty("attrs")]
        public LabelAttrs Attrs { get; set; }

        [JsonProperty("position")]
        public PositionUnion Position { get; set; }
    }

    public partial class LabelAttrs
    {
        [JsonProperty("text")]
        public Text Text { get; set; }
    }

    public partial class Text
    {
        [JsonProperty("text")]
        public string TextText { get; set; }

        [JsonProperty("fill", NullValueHandling = NullValueHandling.Ignore)]
        public HeaderTextFill? Fill { get; set; }
    }

    public partial class PositionPosition
    {
        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("angle")]
        public long Angle { get; set; }
    }

    public partial class VertexClass
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }

    public partial class Size
    {
        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }
    }

    public partial class Source
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("anchor", NullValueHandling = NullValueHandling.Ignore)]
        public Anchor Anchor { get; set; }
    }

    public partial class Anchor
    {
        [JsonProperty("name")]
        public AnchorName Name { get; set; }

        [JsonProperty("args")]
        public Args Args { get; set; }
    }

    public partial class Args
    {
        [JsonProperty("dx")]
        public string Dx { get; set; }

        [JsonProperty("dy")]
        public string Dy { get; set; }

        [JsonProperty("rotate")]
        public bool Rotate { get; set; }
    }

    public enum BodyFill { Dcd7D7, Feb663, The7C68Fc, Transparent };

    public enum Stroke { The31D0C6, Transparent };

    public enum FontFamily { RobotoCondensed };

    public enum FontWeight { Normal };

    public enum HeaderTextFill { C6C7E2, F6F6F6, The222138, The4B4A67 };

    public enum D { M0000 };

    public enum DataTooltipPosition { Left };

    public enum DataTooltipPositionSelector { JointStencil };

    public enum ConnectorName { Normal, Orthogonal, Rounded };

    public enum AnchorName { TopLeft };

    public enum TypeEnum { AppLink, StandardEllipse, StandardHeaderedRectangle, StandardPolygon, StandardRectangle };

    public partial struct PositionUnion
    {
        public double? Double;
        public PositionPosition PositionPosition;

        public static implicit operator PositionUnion(double Double)
        {
            return new PositionUnion { Double = Double };
        }
        public static implicit operator PositionUnion(PositionPosition PositionPosition)
        {
            return new PositionUnion { PositionPosition = PositionPosition };
        }
    }

    public static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                BodyFillConverter.Singleton,
                StrokeConverter.Singleton,
                FontFamilyConverter.Singleton,
                FontWeightConverter.Singleton,
                HeaderTextFillConverter.Singleton,
                DConverter.Singleton,
                DataTooltipPositionConverter.Singleton,
                DataTooltipPositionSelectorConverter.Singleton,
                ConnectorNameConverter.Singleton,
                PositionUnionConverter.Singleton,
                AnchorNameConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class BodyFillConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(BodyFill) || t == typeof(BodyFill?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "#7c68fc":
                    return BodyFill.The7C68Fc;
                case "#dcd7d7":
                    return BodyFill.Dcd7D7;
                case "#feb663":
                    return BodyFill.Feb663;
                case "transparent":
                    return BodyFill.Transparent;
            }
            throw new Exception("Cannot unmarshal type BodyFill");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BodyFill)untypedValue;
            switch (value)
            {
                case BodyFill.The7C68Fc://蓝色
                    serializer.Serialize(writer, "#7c68fc");
                    return;
                case BodyFill.Dcd7D7://灰色
                    serializer.Serialize(writer, "#dcd7d7");
                    return;
                case BodyFill.Feb663://橘色
                    serializer.Serialize(writer, "#feb663");
                    return;
                case BodyFill.Transparent://透明
                    serializer.Serialize(writer, "transparent");
                    return;
            }
            throw new Exception("Cannot marshal type BodyFill");
        }

        public static readonly BodyFillConverter Singleton = new BodyFillConverter();
    }

    internal class StrokeConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(Stroke) || t == typeof(Stroke?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "#31d0c6"://绿色
                    return Stroke.The31D0C6;
                case "transparent":
                    return Stroke.Transparent;
            }
            throw new Exception("Cannot unmarshal type Stroke");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Stroke)untypedValue;
            switch (value)
            {
                case Stroke.The31D0C6://绿色
                    serializer.Serialize(writer, "#31d0c6");
                    return;
                case Stroke.Transparent:
                    serializer.Serialize(writer, "transparent");
                    return;
            }
            throw new Exception("Cannot marshal type Stroke");
        }

        public static readonly StrokeConverter Singleton = new StrokeConverter();
    }

    internal class FontFamilyConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(FontFamily) || t == typeof(FontFamily?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Roboto Condensed")
            {
                return FontFamily.RobotoCondensed;
            }
            throw new Exception("Cannot unmarshal type FontFamily");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FontFamily)untypedValue;
            if (value == FontFamily.RobotoCondensed)
            {
                serializer.Serialize(writer, "Roboto Condensed");
                return;
            }
            throw new Exception("Cannot marshal type FontFamily");
        }

        public static readonly FontFamilyConverter Singleton = new FontFamilyConverter();
    }

    internal class FontWeightConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(FontWeight) || t == typeof(FontWeight?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Normal")
            {
                return FontWeight.Normal;
            }
            throw new Exception("Cannot unmarshal type FontWeight");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FontWeight)untypedValue;
            if (value == FontWeight.Normal)
            {
                serializer.Serialize(writer, "Normal");
                return;
            }
            throw new Exception("Cannot marshal type FontWeight");
        }

        public static readonly FontWeightConverter Singleton = new FontWeightConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(long) || t == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class HeaderTextFillConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(HeaderTextFill) || t == typeof(HeaderTextFill?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "#222138"://黑色 节点
                    return HeaderTextFill.The222138;
                case "#4b4a67"://黑色 标题
                    return HeaderTextFill.The4B4A67;
                case "#c6c7e2"://蓝色
                    return HeaderTextFill.C6C7E2;
                case "#f6f6f6"://白色
                    return HeaderTextFill.F6F6F6;
            }
            throw new Exception("Cannot unmarshal type HeaderTextFill");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (HeaderTextFill)untypedValue;
            switch (value)
            {
                case HeaderTextFill.The222138://黑色 节点
                    serializer.Serialize(writer, "#222138");
                    return;
                case HeaderTextFill.The4B4A67://黑色 标题
                    serializer.Serialize(writer, "#4b4a67");
                    return;
                case HeaderTextFill.C6C7E2://蓝色
                    serializer.Serialize(writer, "#c6c7e2");
                    return;
                case HeaderTextFill.F6F6F6://白色
                    serializer.Serialize(writer, "#f6f6f6");
                    return;
            }
            throw new Exception("Cannot marshal type HeaderTextFill");
        }

        public static readonly HeaderTextFillConverter Singleton = new HeaderTextFillConverter();
    }

    internal class DConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(D) || t == typeof(D?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "M 0 0 0 0")
            {
                return D.M0000;
            }
            throw new Exception("Cannot unmarshal type D");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (D)untypedValue;
            if (value == D.M0000)
            {
                serializer.Serialize(writer, "M 0 0 0 0");
                return;
            }
            throw new Exception("Cannot marshal type D");
        }

        public static readonly DConverter Singleton = new DConverter();
    }

    internal class DataTooltipPositionConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(DataTooltipPosition) || t == typeof(DataTooltipPosition?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "left")
            {
                return DataTooltipPosition.Left;
            }
            throw new Exception("Cannot unmarshal type DataTooltipPosition");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DataTooltipPosition)untypedValue;
            if (value == DataTooltipPosition.Left)
            {
                serializer.Serialize(writer, "left");
                return;
            }
            throw new Exception("Cannot marshal type DataTooltipPosition");
        }

        public static readonly DataTooltipPositionConverter Singleton = new DataTooltipPositionConverter();
    }

    internal class DataTooltipPositionSelectorConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(DataTooltipPositionSelector) || t == typeof(DataTooltipPositionSelector?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == ".joint-stencil")
            {
                return DataTooltipPositionSelector.JointStencil;
            }
            throw new Exception("Cannot unmarshal type DataTooltipPositionSelector");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DataTooltipPositionSelector)untypedValue;
            if (value == DataTooltipPositionSelector.JointStencil)
            {
                serializer.Serialize(writer, ".joint-stencil");
                return;
            }
            throw new Exception("Cannot marshal type DataTooltipPositionSelector");
        }

        public static readonly DataTooltipPositionSelectorConverter Singleton = new DataTooltipPositionSelectorConverter();
    }

    internal class ConnectorNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ConnectorName) || t == typeof(ConnectorName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "normal":
                    return ConnectorName.Normal;
                case "orthogonal":
                    return ConnectorName.Orthogonal;
                case "rounded":
                    return ConnectorName.Rounded;
            }
            throw new Exception("Cannot unmarshal type ConnectorName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ConnectorName)untypedValue;
            switch (value)
            {
                case ConnectorName.Normal:
                    serializer.Serialize(writer, "normal");
                    return;
                case ConnectorName.Orthogonal:
                    serializer.Serialize(writer, "orthogonal");
                    return;
                case ConnectorName.Rounded:
                    serializer.Serialize(writer, "rounded");
                    return;
            }
            throw new Exception("Cannot marshal type ConnectorName");
        }

        public static readonly ConnectorNameConverter Singleton = new ConnectorNameConverter();
    }

    internal class PositionUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(PositionUnion) || t == typeof(PositionUnion?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    var doubleValue = serializer.Deserialize<double>(reader);
                    return new PositionUnion { Double = doubleValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<PositionPosition>(reader);
                    return new PositionUnion { PositionPosition = objectValue };
            }
            throw new Exception("Cannot unmarshal type PositionUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (PositionUnion)untypedValue;
            if (value.Double != null)
            {
                serializer.Serialize(writer, value.Double.Value);
                return;
            }
            if (value.PositionPosition != null)
            {
                serializer.Serialize(writer, value.PositionPosition);
                return;
            }
            throw new Exception("Cannot marshal type PositionUnion");
        }

        public static readonly PositionUnionConverter Singleton = new PositionUnionConverter();
    }

    internal class AnchorNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(AnchorName) || t == typeof(AnchorName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "topLeft")
            {
                return AnchorName.TopLeft;
            }
            throw new Exception("Cannot unmarshal type AnchorName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AnchorName)untypedValue;
            if (value == AnchorName.TopLeft)
            {
                serializer.Serialize(writer, "topLeft");
                return;
            }
            throw new Exception("Cannot marshal type AnchorName");
        }

        public static readonly AnchorNameConverter Singleton = new AnchorNameConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(TypeEnum) || t == typeof(TypeEnum?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "app.Link":
                    return TypeEnum.AppLink;
                case "standard.Ellipse"://圆形
                    return TypeEnum.StandardEllipse;
                case "standard.HeaderedRectangle"://图例
                    return TypeEnum.StandardHeaderedRectangle;
                case "standard.Polygon"://菱形
                    return TypeEnum.StandardPolygon;
                case "standard.Rectangle"://方形
                    return TypeEnum.StandardRectangle;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.AppLink:
                    serializer.Serialize(writer, "app.Link");
                    return;
                case TypeEnum.StandardEllipse:
                    serializer.Serialize(writer, "standard.Ellipse");
                    return;
                case TypeEnum.StandardHeaderedRectangle:
                    serializer.Serialize(writer, "standard.HeaderedRectangle");
                    return;
                case TypeEnum.StandardPolygon:
                    serializer.Serialize(writer, "standard.Polygon");
                    return;
                case TypeEnum.StandardRectangle:
                    serializer.Serialize(writer, "standard.Rectangle");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}