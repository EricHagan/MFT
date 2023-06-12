using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MFT
{
    public class ProcessorChain : IEnumerable<IProcessor>, IList<IProcessor>, IProcessor
    {
        public ProcessorChain() { }

        public ProcessorChain(ProcessorChain x)
        {
            ID = x.ID;
            Name= x.Name;
            chain = new List<IProcessor>();
            foreach (var processor in x.chain)
                chain.Add(ProcessorFactory.GetCopyOf(processor));
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public Exposure Process(Exposure data)
        {
            var newSpectrum = Process(data.Spectrum);
            return new Exposure(
                newSpectrum,
                data.TimeStamp,
                data.Normalized,
                this);
        }

        List<IProcessor> chain = new List<IProcessor>();

        public IProcessor this[int index] { get => ((IList<IProcessor>)chain)[index]; set => ((IList<IProcessor>)chain)[index] = value; }

        public int Count => ((ICollection<IProcessor>)chain).Count;

        public bool IsReadOnly => ((ICollection<IProcessor>)chain).IsReadOnly;

        public ProcessorFactory.Types Type => ProcessorFactory.Types.CHAIN;

        public void Add(IProcessor item)
        {
            ((ICollection<IProcessor>)chain).Add(item);
        }

        public void Clear()
        {
            ((ICollection<IProcessor>)chain).Clear();
        }

        public bool Contains(IProcessor item)
        {
            return ((ICollection<IProcessor>)chain).Contains(item);
        }

        public void CopyTo(IProcessor[] array, int arrayIndex)
        {
            ((ICollection<IProcessor>)chain).CopyTo(array, arrayIndex);
        }

        public string GetDescription()
        {
            var output = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Name))
                output.Append(Name + ":");
            else
                output.Append("Chain: ");
            if (chain == null || chain.Count == 0)
                output.Append("Empty");
            foreach (IProcessor processor in chain)
            {
                output.Append(processor.GetDescription() + " ");
            }
            output.Remove(output.Length - 1, 1);
            return output.ToString();
        }

        public IEnumerator<IProcessor> GetEnumerator()
        {
            return ((IEnumerable<IProcessor>)chain).GetEnumerator();
        }

        public int IndexOf(IProcessor item)
        {
            return ((IList<IProcessor>)chain).IndexOf(item);
        }

        public void Insert(int index, IProcessor item)
        {
            ((IList<IProcessor>)chain).Insert(index, item);
        }

        public Spectrum Process(Spectrum data)
        {
            if (chain == null)
                return data;
            if (chain.Count == 0)
                return data;
            Spectrum spectrum = data;
            foreach (IProcessor processor in chain)
            {
                spectrum = processor.Process(spectrum);
            }
            return spectrum;
        }

        public bool Remove(IProcessor item)
        {
            return ((ICollection<IProcessor>)chain).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<IProcessor>)chain).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)chain).GetEnumerator();
        }
    }
}
