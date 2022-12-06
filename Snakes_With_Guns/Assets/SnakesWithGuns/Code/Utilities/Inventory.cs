using System.Collections.Generic;

namespace SnakesWithGuns.Utilities
{
    public class Inventory<T>
    {
        private Dictionary<T, int> _amountByItem;

        public Inventory()
        {
            _amountByItem = new Dictionary<T, int>();
        }

        public bool ContainsAmount(T item, int amount = 1)
        {
            return GetAmount(item) >= amount;
        }

        public int GetAmount(T item)
        {
            if (!_amountByItem.ContainsKey(item))
                return 0;

            return _amountByItem[item];
        }

        public void SetAmount(T item, int amount)
        {
            int oldAmount = GetAmount(item);
            
            if (!_amountByItem.ContainsKey(item))
            {
                _amountByItem.Add(item, amount);
            }
            else
            {
                _amountByItem[item] = amount;
            }
        }
    }
}