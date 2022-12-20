using BrainUp.Models;

namespace BrainUp.ViewModels
{
    public class RankViewModel
    {
        public int Id { get; set; }

        public int? Value { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CourceId { get; set; }

        public int? Value1 { get; set; }
        public int? Value2{ get; set; }
        public int? Value3 { get; set; }
        public int? Value4 { get; set; }
        public int? Value5 { get; set; }

        public Rank ToRank()
        { 
            Rank rank = new Rank();
            rank.Id = Id;
            rank.CreatedDate = CreatedDate;
            rank.CourceId = CourceId;

            if (Value1 is not null && Value1 != 0)
            {
                rank.Value = (int)Value1;
            }

            if (Value2 is not null && Value2 != 0)
            {
                rank.Value = (int)Value2;
            }

            if (Value3 is not null && Value3 != 0)
            {
                rank.Value = (int)Value3;
            }

            if (Value4 is not null && Value4 != 0)
            {
                rank.Value = (int)Value4;
            }

            if (Value5 is not null && Value5 != 0)
            {
                rank.Value = (int)Value5;
            }

            return rank;
        }

        public void FromRank(Rank rank)
        { 
            Id = rank.Id;
            CreatedDate = rank.CreatedDate; 
            CourceId = rank.CourceId;
            Value = rank.Value;
        }

    }
}
