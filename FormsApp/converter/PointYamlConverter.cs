using Lab1;
using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FormsApp
{
    public class PointYamlConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(Point) || type == typeof(Point3D);
        }

        public object ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
        {
            parser.Consume<MappingStart>();

            // Инициализируем значения
            int x = 0;
            int y = 0;
            int? z = null;

            // Считываем данные до тех пор, пока не достигнем конца
            while (parser.Current != null && !(parser.Current is MappingEnd))
            {
                var key = parser.Consume<Scalar>().Value.ToLower(); // Считываем ключ и переводим его в нижний регистр

                switch (key)
                {
                    case "x":
                        x = int.Parse((parser.Consume<Scalar>()).Value);
                        break;
                    case "y":
                        y = int.Parse((parser.Consume<Scalar>()).Value);
                        break;
                    case "z":
                        z = int.Parse((parser.Consume<Scalar>()).Value);
                        break;
                    default:
                        throw new InvalidOperationException($"Unexpected key: {key}");
                }
            }

            parser.Consume<MappingEnd>(); // Завершаем считывание

            // Создаем соответствующий объект
            if (z.HasValue)
            {
                return new Point3D { X = x, Y = y, Z = z.Value }; // Возвращаем Point3D, если Z присутствует
            }

            return new Point { X = x, Y = y }; // В противном случае возвращаем Point
        }


        public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer serializer)
        {
            emitter.Emit(new MappingStart()); // Используем MappingStart

            if (value is Point point)
            {
                // Записываем X и Y
                emitter.Emit(new Scalar("x"));
                emitter.Emit(new Scalar(point.X.ToString()));
                emitter.Emit(new Scalar("y"));
                emitter.Emit(new Scalar(point.Y.ToString()));

                // Если это Point3D, записываем также Z
                if (point is Point3D point3D)
                {
                    emitter.Emit(new Scalar("z"));
                    emitter.Emit(new Scalar(point3D.Z.ToString()));
                }
            }

            emitter.Emit(new MappingEnd()); // Указываем конец
        }
    }
}
