using System;
using System.Collections.Generic;
using System.Linq;

namespace Cubo.Core.Domain
{
    public class Bucket : Entity
    {
        private ISet<Item> _items = new HashSet<Item>();
        public string Name { get; protected set; }
        public IEnumerable<Item> Items { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Bucket()
        {

        }

        public Bucket(Guid id, string name)
        {
            Id = id;
            Name = name.ToLowerInvariant();
        }

        public void AddItem(string key, string value)
        {
            var fixedKey = key.ToLowerInvariant();

            if (Items.Any(x => x.Key == fixedKey))
                throw new Exception($"Item: {key} already exists in bucket: {Name}");

            _items.Add(new Item(key, value));
        }

        public void RemoveItem(string key)
        {
            var item = GetItemOrFail(key);
            _items.Remove(item);
        }

        public Item GetItemOrFail(string key)
        {
            var fixedKey = key.ToLowerInvariant();
            var item = Items.SingleOrDefault(x => x.Key == fixedKey);

            if (item == null)
                throw new Exception($"Item: {key} could not be found in bucket: {Name}");

            return item;
        }
    }
}