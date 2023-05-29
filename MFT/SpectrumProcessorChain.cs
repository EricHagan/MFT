using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MFT
{
    public class SpectrumProcessorChain : IEnumerable<ISpectrumProcessor>, IList<ISpectrumProcessor>, ISpectrumProcessor
    {
        public SpectrumProcessorChain() { }

        public SpectrumProcessorChain(SpectrumProcessorChain x)
        {
            ID = x.ID;
            Name= x.Name;
            chain = new List<ISpectrumProcessor>();
            foreach (var processor in x.chain)
                chain.Add(SpectrumProcessorFactory.GetCopyOf(processor));
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

        List<ISpectrumProcessor> chain = new List<ISpectrumProcessor>();

        public ISpectrumProcessor this[int index] { get => ((IList<ISpectrumProcessor>)chain)[index]; set => ((IList<ISpectrumProcessor>)chain)[index] = value; }

        public int Count => ((ICollection<ISpectrumProcessor>)chain).Count;

        public bool IsReadOnly => ((ICollection<ISpectrumProcessor>)chain).IsReadOnly;

        public SpectrumProcessorFactory.Types Type => SpectrumProcessorFactory.Types.CHAIN;

        public void Add(ISpectrumProcessor item)
        {
            ((ICollection<ISpectrumProcessor>)chain).Add(item);
        }

        public void Clear()
        {
            ((ICollection<ISpectrumProcessor>)chain).Clear();
        }

        public bool Contains(ISpectrumProcessor item)
        {
            return ((ICollection<ISpectrumProcessor>)chain).Contains(item);
        }

        public void CopyTo(ISpectrumProcessor[] array, int arrayIndex)
        {
            ((ICollection<ISpectrumProcessor>)chain).CopyTo(array, arrayIndex);
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
            foreach (ISpectrumProcessor processor in chain)
            {
                output.Append(processor.GetDescription() + " ");
            }
            output.Remove(output.Length - 1, 1);
            return output.ToString();
        }

        public IEnumerator<ISpectrumProcessor> GetEnumerator()
        {
            return ((IEnumerable<ISpectrumProcessor>)chain).GetEnumerator();
        }

        public int IndexOf(ISpectrumProcessor item)
        {
            return ((IList<ISpectrumProcessor>)chain).IndexOf(item);
        }

        public void Insert(int index, ISpectrumProcessor item)
        {
            ((IList<ISpectrumProcessor>)chain).Insert(index, item);
        }

        public Spectrum Process(Spectrum data)
        {
            if (chain == null)
                return data;
            if (chain.Count == 0)
                return data;
            Spectrum spectrum = data;
            foreach (ISpectrumProcessor processor in chain)
            {
                spectrum = processor.Process(spectrum);
            }
            return spectrum;
        }

        public bool Remove(ISpectrumProcessor item)
        {
            return ((ICollection<ISpectrumProcessor>)chain).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<ISpectrumProcessor>)chain).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)chain).GetEnumerator();
        }
    }
}
