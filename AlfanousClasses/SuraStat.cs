namespace AlfanousWP7.AlfanousClasses
{
    public class SuraStat:Stat
    {
        public int? Ayas { get; set; }
        public override string ToString()
        {
            start = string.Format("������ ���� {0}� ", NumberToString(Ayas.Value, "���", "�����", "����"));
            return base.ToString();
        }
    }
}