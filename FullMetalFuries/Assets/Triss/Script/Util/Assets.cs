using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset
{
    namespace PlayerPrefab
    {
        using PPrefab = PlayerPrefabKeysAndValue;


        class PlayerPrefabKeysAndValue
        {
            public enum KeyTypes
            {
                Item,
                KeyCount
            }
            public static readonly string[][] Keys =
            {
                ItemKeys
            };

            public enum ItemTypes
            {
                Coin,
                ItemCount
            }
            public static readonly string[] ItemKeys =
            {
                "Coin"
            };

            // 초기화는 외부에서 파일을 받아 사용할 예정

        }
    }
}
