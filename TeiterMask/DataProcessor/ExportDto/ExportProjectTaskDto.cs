namespace TeisterMask.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Task")]
    public class ExportProjectTaskDto
    {
        [XmlElement("Name")]
        public string TaskName { get; set; }

        [XmlElement("Label")]
        public string LabelString { get; set; }
    }
}
