using JsonConvertor.Components.Convertors;
using JsonConvertor.Components.Readers;
using JsonConvertor.Components.Writers;
using JsonConvertor.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace JsonConvertor
{
    public class ComponentsManager : IComponentsManager
    {
        public List<IInputReader> Readers { get; set; }
        public List<IConvertor> Convertors { get; set; }
        public List<IOutputWriter> Writers { get; set; }

        public ComponentsManager()
        {
            Readers = new List<IInputReader>();
            Convertors = new List<IConvertor>();
            Writers = new List<IOutputWriter>();

            RegisterDefaultComponents();
        }

        public void RegisterDefaultComponents()
        {
            List<IInputReader> readers = new List<IInputReader>()
            {
                new FileReader()
            };

            List<IConvertor> convertors = new List<IConvertor>()
            {
                new JsonToPathsConvertor(),
                new PathsToJsonConvertor()
            };

            List<IOutputWriter> writers = new List<IOutputWriter>()
            {
                new JsonWriter(),
                new PathsWriter()
            };

            RegisterReaders(readers);
            RegisterConvertors(convertors);
            RegisterWriters(writers);
        }

        public void RegisterReaders(IEnumerable<IInputReader> readers)
        {
            Readers.Clear();
            Readers.AddRange(readers);
        }

        public void RegisterConvertors(IEnumerable<IConvertor> convertors)
        {
            Convertors.Clear();
            Convertors.AddRange(convertors);
        }

        public void RegisterWriters(IEnumerable<IOutputWriter> writers)
        {
            Writers.Clear();
            Writers.AddRange(writers);
        }

        public IInputReader GetReader(ConsoleArgs args)
        {
            return Readers.FirstOrDefault(r => r.FitsConversionType(args));
        }

        public IConvertor GetConvertor(ConsoleArgs args)
        {
            return Convertors.FirstOrDefault(c => c.FitsConversionType(args));
        }

        public IOutputWriter GetWriter(ConsoleArgs args)
        {
            return Writers.FirstOrDefault(w => w.FitsConversionType(args));
        }
    }
}
