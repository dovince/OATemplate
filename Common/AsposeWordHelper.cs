using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ZWL.Common
{
    /// <summary>
    /// word文档操作辅助类
    /// </summary>
    public class AsposeWordHelper
    {
        /// <summary>
        /// Word
        /// </summary>
        private Document _doc;
        public Document Doc { get { return _doc; } }
        public void SetDoc(Document doc)
        {
            _doc = doc;
        }

        /// <summary>
        /// 替换自定义文本
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Replace<TEntity>(TEntity entity, MarkTypeEnum markTypeEnum)
        {
            var type = entity.GetType();
            var properties = type.GetProperties();
            foreach (var item in properties)
            {
                _doc.Range.Replace(ConvertMarkType(item.Name, markTypeEnum), item.GetValue(entity, null).ToString(), new Aspose.Words.Replacing.FindReplaceOptions());
            }
        }

        /// <summary>
        /// 替换自定义文本
        /// </summary>
        /// <param name="dic"></param>
        public void Replace(Dictionary<string, string> dic, MarkTypeEnum markTypeEnum)
        {

            foreach (var item in dic)
            {
                _doc.Range.Replace(ConvertMarkType(item.Key, markTypeEnum), item.Value, new Aspose.Words.Replacing.FindReplaceOptions());
            }
        }

        /// <summary>
        /// 转成标签 [[]] , {{}}
        /// </summary>
        /// <param name="name"></param>
        /// <param name="markTypeEnum"></param>
        /// <returns></returns>
        public static string ConvertMarkType(string name, MarkTypeEnum markTypeEnum = MarkTypeEnum.DEFAULT)
        {
            switch (markTypeEnum)
            {
                case MarkTypeEnum.CURLYBRACES:
                    return MarkTypeCurlyBraces(name);
                default:
                    return MarkTypeDefault(name);
            }
        }

        /// <summary>
        /// 返回[[name]]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string MarkTypeDefault(string name)
        {
            return $"[[{name}]]";
        }

        /// <summary>
        /// 返回{{name}}
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string MarkTypeCurlyBraces(string name)
        {
            return $"{{{{{name}}}}}";
        }
        /// <summary>
        /// 文本域处理泛型集合赋值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Execute<TEntity>(TEntity entity)
        {
            var type = entity.GetType();
            var properties = type.GetProperties();
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            foreach (var item in properties)
            {
                names.Add(item.Name);
                var val = item.GetValue(entity, null);
                if (val == null)
                    values.Add(null);
                else
                    values.Add(item.GetValue(entity, null).ToString());
            }
            _doc.MailMerge.Execute(names.ToArray(), values.ToArray());
        }

        /// <summary>
        /// 文本域处理键值对赋值
        /// </summary>
        /// <param name="dic"></param>
        public void Execute(Dictionary<string, string> dic)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            foreach (var item in dic)
            {
                names.Add(item.Key);
                values.Add(item.Value);
            }
            _doc.MailMerge.Execute(names.ToArray(), values.ToArray());
        }
        /// <summary>
        /// 文本域赋值用法
        /// </summary>
        /// <param name="fieldNames">key</param>
        /// <param name="fieldValues">value</param>
        public void ExecuteField(string[] fieldNames, object[] fieldValues)
        {
            _doc.MailMerge.Execute(fieldNames, fieldValues);
        }
        /// <summary>
        /// 文本域赋值用法
        /// </summary>
        /// <param name="fieldNames">key</param>
        /// <param name="fieldValues">value</param>
        public void ExecuteField(string fieldName, object fieldValue)
        {
            _doc.MailMerge.Execute(new string[] { fieldName }, new object[] { fieldValue });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath, SaveFormat saveFormat = SaveFormat.Doc)
        {
            _doc.Save(filePath, saveFormat);
        }

        /// <summary>
        /// 基于模版新建Word文件
        /// </summary>
        /// <param name="path">模板路径</param>
        public void OpenTempelte(string tempPath)
        {
            _doc = new Document(tempPath);
        }

        /// <summary>
        /// 书签赋值用法
        /// </summary>
        /// <param name="LabelId">书签名</param>
        /// <param name="Content">内容</param>
        public void WriteBookMark(string LabelId, string Content)
        {
            if (_doc.Range.Bookmarks[LabelId] != null)
            {
                _doc.Range.Bookmarks[LabelId].Text = Content;
            }
        }

        /// <summary>
        /// 书签赋值用法
        /// </summary>
        /// <param name="entity">键值对</param>
        public void WriteBookMark(Dictionary<string, string> dic)
        {
            foreach (var item in dic)
            {
                if (_doc.Range.Bookmarks[item.Key] != null)
                {
                    _doc.Range.Bookmarks[item.Key].Text = item.Value;
                }
            }
        }

        /// <summary>
        /// 列表赋值用法
        /// </summary>
        /// <param name="dt"></param>
        public void WriteTable(DataTable dt)
        {
            _doc.MailMerge.ExecuteWithRegions(dt);
        }

        /// <summary>
        /// 不可编辑受保护，需输入密码
        /// </summary>
        /// <param name="pwd">密码</param>
        public void NoEdit(string pwd)
        {
            _doc.Protect(ProtectionType.ReadOnly, pwd);
        }

        /// <summary>
        /// 只读
        /// </summary>
        public void ReadOnly()
        {
            _doc.Protect(ProtectionType.ReadOnly);
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="filename">文件路径+文件名</param>
        /// <param name="field">文本域名</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImage(string filename, string field, double width = 70, double height = 70)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(filename);
            shape.Width = width;//设置宽和高
            shape.Height = height;
            shape.WrapType = WrapType.Inline;
            shape.BehindText = true;

            builder.MoveToMergeField(field);
            builder.InsertNode(shape);
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="filename">文件路径+文件名</param>
        /// <param name="field">文本域名</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImage(string filename, string field, WrapType wrapType = WrapType.None, double width = 70, double height = 70)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(filename);
            shape.Width = width;//设置宽和高
            shape.Height = height;
            shape.WrapType = wrapType;
            shape.BehindText = true;

            builder.MoveToMergeField(field);
            builder.InsertNode(shape);
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="filename">文件路径+文件名</param>
        /// <param name="field">文本域名</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImage(string filename, string field)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(filename);
            shape.WrapType = WrapType.Inline;
            shape.BehindText = true;
            builder.MoveToMergeField(field);
            builder.InsertNode(shape);
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="filename">文件路径+文件名</param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImage(string filename, double left = 0, double top = 0, double width = 70, double height = 70)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(filename);
            shape.Width = width;//设置宽和高
            shape.Height = height;
            shape.Top = top;
            shape.Left = left;
            builder.InsertNode(shape);
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="field">文本域名</param>
        /// <param name="stream">流</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImageByStream(string field, Stream stream, double width = 70, double height = 70)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(stream);
            shape.Width = width;//设置宽和高
            shape.Height = height;
            shape.WrapType = WrapType.Inline;
            shape.BehindText = false;
            builder.MoveToMergeField(field);
            builder.InsertNode(shape);
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="stream">文件</param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImage(string filename, double left = 0, double top = 0)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(filename);
            //shape.Width = width;//设置宽和高
            //shape.Height = height;
            shape.Top = top;
            shape.Left = left;
            builder.InsertNode(shape);
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="stream">文件</param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImage(Stream stream, double left = 0, double top = 0)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(stream);
            //shape.Width = width;//设置宽和高
            //shape.Height = height;
            shape.Top = top;
            shape.Left = left;
            builder.InsertNode(shape);

        }
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="field">文本域名</param>
        /// <param name="stream">流</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void AddImage2(string field, string filename)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            Shape shape = new Shape(_doc, ShapeType.Image);
            shape.ImageData.SetImage(filename);
            builder.MoveToMergeField(field);
            builder.InsertNode(shape);
        }

        /// <summary>
        /// 通过流导出word文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public HttpResponseMessage ExportWord(string fileName, SaveFormat saveFormat = SaveFormat.Doc)
        {
            var stream = new MemoryStream();
            _doc.Save(stream, saveFormat);
            fileName += DateTime.Now.ToString("yyyyMMddHHmmss");
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            string type = saveFormat == SaveFormat.Doc ? ".doc" : ".docx";
            result.Content.Headers.ContentDisposition.FileName = fileName + type;
            return result;
        }

        /// <summary>
        /// 通过流导出pdf文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public HttpResponseMessage ExportPdf(string fileName)
        {
            var stream = new MemoryStream();
            _doc.Save(stream, SaveFormat.Pdf);
            fileName += DateTime.Now.ToString("yyyyMMddHHmmss");
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName + ".pdf";
            return result;
        }

        /// <summary>
        /// 基于模版新建Word文件
        /// </summary>
        /// <param name="stream">模板流</param>
        public void OpenTempelteStream(Stream stream)
        {
            _doc = new Document(stream);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveStream(Stream stream, SaveFormat saveFormat = SaveFormat.Doc)
        {
            _doc.Save(stream, saveFormat);
        }
        public SaveFormat GetSaveFormat(string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                return SaveFormat.Pdf;
            switch (ext.ToLower())
            {
                case ".pdf": return SaveFormat.Pdf;
                case ".docx": return SaveFormat.Docx;
                case ".doc": return SaveFormat.Doc;
                case ".dot": return SaveFormat.Dot;
                case ".html": return SaveFormat.Html;
                case ".png": return SaveFormat.Png;
                case ".xps": return SaveFormat.Xps;
                case ".tiff": return SaveFormat.Tiff;
                case ".wordml": return SaveFormat.WordML;
                case ".text": return SaveFormat.Text;
                case ".jpeg": return SaveFormat.Jpeg;
                case ".bmp": return SaveFormat.Bmp;
                default: return SaveFormat.Pdf;
            }
        }

        /// <summary>
        /// 列表赋值用法(多集合)
        /// </summary>
        /// <param name="dt"></param>
        public void WriteDataSet(DataSet ds)
        {
            _doc.MailMerge.ExecuteWithRegions(ds);
        }
        public NodeCollection GetNodeCollection()
        {
            return _doc.GetChildNodes(NodeType.Table, true);
        }
        public Aspose.Words.DocumentBuilder GetDocumentBuilder()
        {
            return new Aspose.Words.DocumentBuilder(_doc);
        }
        public Aspose.Words.Tables.Cell CreateCell(string value)
        {
            Aspose.Words.Tables.Cell c1 = new Aspose.Words.Tables.Cell(_doc);
            Aspose.Words.Paragraph p = new Paragraph(_doc);
            p.AppendChild(new Run(_doc, value));
            c1.AppendChild(p);

            return c1;
        }
        public Aspose.Words.Tables.Row CreateRow(int columnCount, string[] columnValues)
        {
            Aspose.Words.Tables.Row r2 = new Aspose.Words.Tables.Row(_doc);
            for (int i = 0; i < columnCount; i++)
            {
                if (columnValues.Length > i)
                {
                    var cell = CreateCell(columnValues[i]);
                    r2.Cells.Add(cell);
                }
                else
                {
                    var cell = CreateCell("");
                    r2.Cells.Add(cell);
                }

            }
            return r2;

        }
        public void RemoveTableRow(int tableIndex, int rowIndex)
        {
            //获取文档中的第一个表.
            var table = (Table)_doc.GetChild(NodeType.Table, tableIndex, true);
            table.Rows.RemoveAt(rowIndex);
        }
        public void RemoveTableRowDesc(int tableIndex, int rowIndex)
        {
            //获取文档中的第一个表.
            var table = (Table)_doc.GetChild(NodeType.Table, tableIndex, true);
            table.Rows.RemoveAt(table.Count - 1 - rowIndex);
        }
    }
    public enum MarkTypeEnum
    {
        /// <summary>
        /// 中括号 [[]]
        /// </summary>
        DEFAULT,
        /// <summary>
        /// 花括号 {{}}
        /// </summary>
        CURLYBRACES
    }
}
