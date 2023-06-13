namespace MultiplicationTable.Models
{
    public class QuestionModel
    {
        public string Question { get; set; }
        private int CorrectAnswer { get; }
        public int? Answer { get; set; }
        public bool IsCorrect => Answer == CorrectAnswer;

        public QuestionModel(int x, int y)
        {
            Question = $"{x} x {y} =";
            CorrectAnswer = x * y;
        }
    }
}