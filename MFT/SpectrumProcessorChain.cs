using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace MFT
{
    internal class SpectrumProcessorChain : IEnumerable<ISpectrumProcessor>, IList<ISpectrumProcessor>, ISpectrumProcessor
    {
        public Spectrum Process(Spectrum data)
        {
            if (chain == null)
                return data;
            if (chain.Count == 0)
                return data;
            Spectrum spectrum = data;
            foreach(ISpectrumProcessor processor in chain)
            {
                spectrum = processor.Process(spectrum);
            }
            return spectrum;
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

        public int ID { get; set; }

        public string Name { get; set; }

        List<ISpectrumProcessor> chain => new List<ISpectrumProcessor>();

        #region IEnumerable

        public IEnumerator<ISpectrumProcessor> GetEnumerator()
        {
            return ((IEnumerable<ISpectrumProcessor>)chain).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)chain).GetEnumerator();
        }

        #endregion

        #region IList

        public int Count => ((ICollection<ISpectrumProcessor>)chain).Count;

        public bool IsReadOnly => ((ICollection<ISpectrumProcessor>)chain).IsReadOnly;

        public ISpectrumProcessor this[int index] { get => ((IList<ISpectrumProcessor>)chain)[index]; set => ((IList<ISpectrumProcessor>)chain)[index] = value; }

        public int IndexOf(ISpectrumProcessor item)
        {
            return ((IList<ISpectrumProcessor>)chain).IndexOf(item);
        }

        public void Insert(int index, ISpectrumProcessor item)
        {
            ((IList<ISpectrumProcessor>)chain).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<ISpectrumProcessor>)chain).RemoveAt(index);
        }

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

        public bool Remove(ISpectrumProcessor item)
        {
            return ((ICollection<ISpectrumProcessor>)chain).Remove(item);
        }

        #endregion
    }
}
