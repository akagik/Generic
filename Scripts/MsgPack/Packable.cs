using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsgPack
{
    // Packable を実装したクラスは引数なしコンストラクタを実装する必要がある
    interface Packable
    {
        void Pack(ObjectPacker packer, MsgPackWriter writer);
        object Unpack(ObjectPacker packer, MsgPackReader reader);
    }
}