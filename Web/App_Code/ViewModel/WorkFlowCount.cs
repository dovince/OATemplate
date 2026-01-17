using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WorkFlowCount 的摘要说明
/// </summary>
public class WorkFlowCount
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public int Total { get; set; }
    public int Processing { get; set; }
    public int Completed { get; set; }
    public int Returned { get; set; }
    public int Rejected { get; set; }
    public int Phone { get; set; }
    public int YiBanCount { get; set; }
}