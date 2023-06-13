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
        private List<AnswerResultModel> AnswerResults { get; set; }

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
            }
        }

        private async Task Next()
        {
            NextQuestion();
            await AnswerField.FocusAsync();
        }

        private string CalcElapsedTime()
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan timeDifference = currentDate - StartDate;
            int elapsedMinutes = (int)timeDifference.TotalMinutes;
            int elapsedSeconds = (int)timeDifference.TotalSeconds % 60;
            return $"{elapsedMinutes} min {elapsedSeconds} sec";
        }

        private void CalcResult()
        {
            AnswerResults = new List<AnswerResultModel>
            {
                new AnswerResultModel
                {
                    Title = "Correct",
                    Count = Questions.Where(x => x.IsCorrect == true).Count(),
                },
                new AnswerResultModel
                {
                    Title = "Incorrect",
                    Count = Questions.Where(x => x.IsCorrect == false).Count(),
                }
            };

            ShowResults = true;
        }

        private string GetPointColor(AnswerResultModel result)
        {
            return result.Title switch
            {
                "Incorrect" => "#e3001b",
                "Correct" => "#00783c",
                _ => "",
            };
        }
    }
}