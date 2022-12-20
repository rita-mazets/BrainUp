using BrainUp.Models;

namespace BrainUp.ViewModels
{
    public class OptionViewModel
    {
        public int? OptionId { get; set; }

        public string? Option { get; set; } = "";

        public bool IsTrue { get; set; }

        public int TaskId { get; set; }

        public Option ToOption()
        { 
            Option option = new Option();
            option.Option1 = this.Option?? "";
            option.Id = this.OptionId?? 0;
            option.TaskId = this.TaskId;
            option.IsTrue = this.IsTrue;

            return option;
        }

        public void FromOption(Option option)
        {
            this.Option = option.Option1;
            this.OptionId = option.Id;
            this.TaskId = option.TaskId;
            this.IsTrue = option.IsTrue;
        }
    }

    
}
