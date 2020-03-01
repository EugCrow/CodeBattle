using SnakeBattle.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WeightDictionaryHelper
    {
        public Dictionary<BoardElement, int> DefaultDictionary
        {
            get
            {
                var dict = new Dictionary<BoardElement, int>();
                foreach (BoardElement element in Enum.GetValues(typeof(BoardElement)))
                {
                    var weight = 0;
                    switch (element)
                    {
                        case BoardElement.Apple:
                            weight = 10;
                            break;
                        case BoardElement.Gold:
                            weight = 10;
                            break;
                    }
                    dict.Add(element, 0);
                }
                return dict;
            }
        }
    }
}
