namespace Bebone.Core.Graphics
{
    public struct AttributeBind(int index, int size, AttributeBindType type, int offset)
    {
        public int Index = index;
        public int Size = size;
        public AttributeBindType Type = type;
        public int Offset = offset;
    }
}
