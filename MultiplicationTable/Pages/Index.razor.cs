using Microsoft.AspNetCore.Components;
using MultiplicationTable.Extensions;
using MultiplicationTable.Models;

namespace MultiplicationTable.Pages
{
    public partial class Index
    {
        public List<QuestionModel> Questions { get; set; }
        public QuestionModel CurrentQuestion { get; set; }
        public DateTime StartDate { get; set; }
        public bool ShowResults { get; set; }
        public ElementReference AnswerField { get; set; }
        private List<AnswerResult> AnswerResults { get; set; } = new List<AnswerResult>();
        
        protected override void OnInitialized()
        {
            Questions = new List<QuestionModel>();
            for (int x = 2; x <= 10; x++)
            {
                for (int y = 2; y <= 10; y++)
                {
                    Questions.Add(new QuestionModel(x, y));
                }
            }
            Questions.Shuffle();
            NextQuestion();
            StartDate = DateTime.Now;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AnswerField.FocusAsync();
            }
        }

        private void NextQuestion()
        {
            CurrentQuestion = Questions.FirstOrDefault(x => x.Answer == null);
            if (CurrentQuestion == null)
            {
                CalcResult();
                ShowResults = true;
            }
        }

        private async Task Next()
        {
            NextQuestion();
            await AnswerField.FocusAsync();
        }

        private void CalcElapsedTime()
        {
            
        }

        private void CalcResult()
        {
            AnswerResults.Clear();
            AnswerResults.Add(new AnswerResult
            {
                Title = "Ok",
                Count = Questions.Where(x => x.IsCorrect == true).Count(),
            });
            AnswerResults.Add(new AnswerResult
            {
                Title = "Wrong",
                Count = Questions.Where(x => x.IsCorrect == false).Count(),
            });
            var ok = Questions.Where(x => x.IsCorrect == true).ToList();
            var wrong = Questions.Where(x => x.IsCorrect == false).ToList();
            ShowResults = true;
        }

      
        private string GetPointColor(AnswerResult order)
        {
            switch (order.Title)
            {
                case "Wrong":
                    return "#e3001b";
                case "Ok":
                    return "#00783c";
                default:
                    return "";
            }
        }
    }

    public class AnswerResult
    {
        public string Title { get; set; }
        public int Count { get; set; }
    }
}