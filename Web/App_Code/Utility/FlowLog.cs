using FSDZ.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using ZWL.BLL;
using ZWL.Common;

/// <summary>
/// FlowLog 的摘要说明
/// </summary>
namespace Utility
{
    public static class FlowLog
    {
        public static string AddLog(object entity, int id, string guid, DateTime timeStamp, string userName, FlowOperation operation = FlowOperation.Add)
        {
            try
            {
                if (string.IsNullOrEmpty(guid))
                    guid = Guid.NewGuid().ToString();
                var list = new List<ZWL.BLL.Flow>();
                var table = entity.GetType().Name;
                var rid = "0";
                try
                {
                    rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
                }
                catch { }
                foreach (var pro in entity.GetType().GetProperties())
                {
                    try
                    {
                        switch (pro.PropertyType.Namespace.ToString().ToLower())
                        {
                            case "system":
                                var newval = string.Empty;
                                if (pro.GetValue(entity, null) != null)
                                    newval = pro.GetValue(entity, null).ToString();
                                var flow = new ZWL.BLL.Flow()
                                {
                                    DataTable = table,
                                    CreatedTime = timeStamp,
                                    TKey = pro.Name,
                                    NewValue = newval,
                                    Operation = (int)operation,
                                    UserName = userName,
                                    LotID = guid,
                                    ParentID = id.ToString(),
                                    RecordID = rid,
                                };
                                list.Add(flow);
                                break;
                            default:

                                break;
                        }
                    }
                    catch { }
                }
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        if (item != null)
                            item.Add();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
            return guid;
        }
        public static string AddLog(object entity, int id)
        {
            return AddLog(entity, id, "", DateTime.Now, PublicMethod.GetUserName());
        }
        public static string AddLog(object entity)
        {
            var rid = "0";
            try
            {
                rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
            }
            catch { }
            return AddLog(entity, PublicMethod.GetInto(rid), "", DateTime.Now, PublicMethod.GetUserName());
        }
        public static IList<ObjectShot> EditShot(object entity)
        {
            var result = new List<ObjectShot>();
            var name = entity.GetType().Name;
            var rid = 0;
            try
            {
                rid = int.Parse(entity.GetType().GetProperty("ID").GetValue(entity, null).ToString());
            }
            catch { }
            var shot = new ObjectShot()
            {
                Name = name,
                ID = rid
            };
            foreach (var pro in entity.GetType().GetProperties())
            {
                try
                {
                    switch (pro.PropertyType.Namespace.ToString().ToLower())
                    {
                        case "system":
                            var v = "";
                            var va = pro.GetValue(entity, null);
                            if (va != null)
                                v = va.ToString();
                            shot.KeyValue.Add(pro.Name, v);
                            break;
                        default:

                            break;
                    }
                }
                catch { }
            }
            result.Add(shot);
            return result;
        }

        public static string EditLog(IList<ObjectShot> shots, object entity)
        {
            var rid = "0";
            try
            {
                rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
            }
            catch { }
            return EditLog(shots, entity, PublicMethod.GetInto(rid), "", DateTime.Now, PublicMethod.GetUserName());
        }
        public static string EditLog(IList<ObjectShot> shots, object entity, int id)
        {
            return EditLog(shots, entity, id, "", DateTime.Now, PublicMethod.GetUserName());
        }
        public static string EditLog(IList<ObjectShot> shots, object entity, int id, string guid, DateTime timeStamp, string userName)
        {
            if (string.IsNullOrEmpty(guid))
                guid = Guid.NewGuid().ToString();
            try
            {
                var list = new List<Flow>();
                var table = entity.GetType().Name;
                var rid = "0";
                try
                {
                    rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
                }
                catch { }
                var shot = shots.FirstOrDefault(r => r.Name == table && r.ID == int.Parse(rid));
                foreach (var pro in entity.GetType().GetProperties())
                {
                    try
                    {
                        switch (pro.PropertyType.Namespace.ToString().ToLower())
                        {
                            case "system":
                                var va = pro.GetValue(entity, null);
                                var v = "";
                                if (va != null)
                                    v = va.ToString();

                                if (shot != null && shot.KeyValue[pro.Name] != v)
                                {
                                    var flow = new Flow()
                                    {
                                        DataTable = table,
                                        CreatedTime = timeStamp,
                                        TKey = pro.Name,
                                        NewValue = v,
                                        OldValue = shot.KeyValue[pro.Name],
                                        Operation = (int)FlowOperation.Edit,
                                        UserName = userName,
                                        LotID = guid,
                                        ParentID = id.ToString(),
                                        RecordID = rid,
                                    };
                                    list.Add(flow);
                                }
                                else if (shot == null)
                                {
                                    var flow = new Flow()
                                    {
                                        DataTable = table,
                                        CreatedTime = timeStamp,
                                        TKey = pro.Name,
                                        NewValue = v,
                                        Operation = (int)FlowOperation.Edit,
                                        UserName = userName,
                                        LotID = guid,
                                        ParentID = id.ToString(),
                                        RecordID = rid,
                                    };
                                    list.Add(flow);
                                }
                                break;
                            default:

                                break;
                        }
                    }
                    catch { }
                }
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        if (item != null)
                            item.Add();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
            return guid;
        }
        public static string DelLog(object entity)
        {
            var rid = "0";
            try
            {
                rid = entity.GetType().GetProperty("ID").GetValue(entity, null).ToString();
            }
            catch { }
            return AddLog(entity, PublicMethod.GetInto(rid), "", DateTime.Now, PublicMethod.GetUserName(), FlowOperation.Delete);
        }
        public static string DelLog(object entity, int id)
        {
            return AddLog(entity, id, "", DateTime.Now, PublicMethod.GetUserName(), FlowOperation.Delete);
        }
        public class LogParameter
        {
            public IList<ObjectShot> Shots { get; set; }
            public object Entity { get; set; }
            public int ID { get; set; }
            public string Guid { get; set; }
            public DateTime Timestamp { get; set; }
            public string UserName { get; set; }
        }

        public class ObjectShot
        {
            public ObjectShot()
            {
                KeyValue = new Dictionary<string, string>();
            }
            public int ID { get; set; }
            public string Name { get; set; }
            public IDictionary<string, string> KeyValue { get; set; }
        }

        public class UserLog
        {
            public UserLog()
            {
                LogShots = new List<LogShot>();
            }
            public string UserName { get; set; }
            public DateTime LogTime { get; set; }
            public FlowOperation Operation { get; set; }
            public IList<LogShot> LogShots { get; set; }

            public class LogShot
            {
                public string Key { get; set; }
                public string OldValue { get; set; }
                public string NewValue { get; set; }
            }
        }
    }

}