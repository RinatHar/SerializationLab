using Lab1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;
using System.Xml.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace FormsApp
{
    public partial class PointForm : Form
    {
        private Point[] points = null;

        public PointForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            points = new Point[5];

            var rnd = new Random();

            for (int i = 0; i < points.Length; i++)
                points[i] = rnd.Next(3) % 2 == 0 ? new Point() : new Point3D();

            listBox.DataSource = points;
        }


        private void btnSort_Click(object sender, EventArgs e)
        {
            if (points == null)
                return;

            Array.Sort(points);

            listBox.DataSource = null;
            listBox.DataSource = points;
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "All|*.*|SOAP|*.soap|XML|*.xml|JSON|*.json|Binary|*.bin|YAML|*.yaml|Points|*.points"
            };

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            using (var fs =
                new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
            {
                switch (Path.GetExtension(dlg.FileName))
                {
                    case ".bin":
                        var bf = new BinaryFormatter();
                        bf.Serialize(fs, points);
                        break;
                    case ".soap":
                        var sf = new SoapFormatter();
                        sf.Serialize(fs, points);
                        break;
                    case ".xml":
                        var xf = new XmlSerializer(typeof(Point[]), new[] { typeof(Point3D) });
                        xf.Serialize(fs, points);
                        break;
                    case ".json":
                        var jf = new JsonSerializer();
                        using (var w = new StreamWriter(fs))
                        {
                            var objList = points.Select(p => new
                            {
                                Type = p.GetType().Name,  // Добавляем название класса
                                Data = p                  // Данные объекта
                            }).ToList();

                            jf.Serialize(w, objList);  // Сериализация списка с указанием типа
                        }
                        break;
                    case ".yaml":
                        var yamlSerializer = new SerializerBuilder()
                            .WithNamingConvention(CamelCaseNamingConvention.Instance)
                            .WithTypeConverter(new PointYamlConverter())
                            .Build();

                        using (var writer = new StreamWriter(fs))
                            yamlSerializer.Serialize(writer, points);
                        break;
                    case ".points":
                        using (var writer = new StreamWriter(fs))
                        {
                            foreach (var point in points)
                            {
                                if (point is Point3D point3D)
                                    writer.WriteLine($"{point3D.X};{point3D.Y};{point3D.Z}");
                                else
                                    writer.WriteLine($"{point.X};{point.Y}");
                            }
                        }
                        break;
                }
            }
        }


        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "All|*.*|SOAP|*.soap|XML|*.xml|JSON|*.json|Binary|*.bin|YAML|*.yaml|Points|*.points"
            };

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            using (var fs =
                new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
            {
                switch (Path.GetExtension(dlg.FileName))
                {
                    case ".bin":
                        var bf = new BinaryFormatter();
                        points = (Point[])bf.Deserialize(fs);
                        break;
                    case ".soap":
                        var sf = new SoapFormatter();
                        points = (Point[])sf.Deserialize(fs);
                        break;
                    case ".xml":
                        var xf = new XmlSerializer(typeof(Point[]), new[] { typeof(Point3D) });
                        points = (Point[])xf.Deserialize(fs);
                        break;
                    case ".json":
                        var jf = new JsonSerializer();
                        using (var r = new StreamReader(fs))
                        {
                            var json = r.ReadToEnd();
                            var deserializedObjects = JsonConvert.DeserializeObject<List<SerializedPoint>>(json);

                            points = deserializedObjects.Select(o =>
                            {
                                if (o.Type == nameof(Point3D))  // Проверяем тип
                                    return JsonConvert.DeserializeObject<Point3D>(o.Data.ToString());
                                else
                                    return JsonConvert.DeserializeObject<Point>(o.Data.ToString());
                            }).ToArray();
                        }
                        break;
                    case ".yaml":
                        var yamlDeserializer = new DeserializerBuilder()
                            .WithNamingConvention(CamelCaseNamingConvention.Instance) // Указания стиля
                            .WithTypeConverter(new PointYamlConverter()) // Использование пользовательского конвертера
                            .Build();

                        using (var reader = new StreamReader(fs))
                            points = yamlDeserializer.Deserialize<Point[]>(reader);
                        break;
                    case ".points":
                        using (var reader = new StreamReader(fs))
                        {
                            var lines = reader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                            points = new Point[lines.Length];

                            for (int i = 0; i < lines.Length; i++)
                            {
                                var parts = lines[i].Split(';');
                                if (parts.Length == 3) // Если есть три значения, это Point3D
                                {
                                    points[i] = new Point3D
                                    {
                                        X = int.Parse(parts[0]),
                                        Y = int.Parse(parts[1]),
                                        Z = int.Parse(parts[2])
                                    };
                                }
                                else if (parts.Length == 2) // Если два значения, это Point
                                {
                                    points[i] = new Point
                                    {
                                        X = int.Parse(parts[0]),
                                        Y = int.Parse(parts[1])
                                    };
                                }
                            }
                        }
                        break;
                }
            }

            listBox.DataSource = null;
            listBox.DataSource = points;
        }

    }
}
