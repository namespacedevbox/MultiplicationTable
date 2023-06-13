using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MultiplicationTable.Pages
{
    public partial class Index
    {
        private List<Questions> questions;
        private bool showResults;
        private Questions currentQuestion;

        private List<AnswerResult> AnswerResults { get; set; } = new List<AnswerResult>();

        public ElementReference AnswerField { get; set; }

        protected override void OnInitialized()
        {
            questions = new List<Questions>();
            for (int x = 2; x <= 10; x++)
            {
                for (int y = 2; y <= 10; y++)
                {
                    questions.Add(new Questions(x, y));
                }
            }
            questions.Shuffle();
            NextQuestion();
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
            currentQuestion = questions.FirstOrDefault(q => q.Answer == null);
            if (currentQuestion == null)
            {
                CalcResult();
                showResults = true;
            }
        }

        private async Task Next()
        {
            NextQuestion();
            await AnswerField.FocusAsync();
        }

        private void CalcResult()
        {
            AnswerResults.Clear();
            AnswerResults.Add(new AnswerResult
            {
                Title = "Ok",
                Count = questions.Where(x => x.IsCorrect == true).Count(),
            });
            AnswerResults.Add(new AnswerResult
            {
                Title = "Wrong",
                Count = questions.Where(x => x.IsCorrect == false).Count(),
            });
            var ok = questions.Where(x => x.IsCorrect == true).ToList();
            var wrong = questions.Where(x => x.IsCorrect == false).ToList();
            showResults = true;
        }

        public async void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await Next();
            }
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