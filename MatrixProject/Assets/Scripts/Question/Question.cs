/// <summary>
/// 問題のテンプレートクラス
/// </summary>
public class Question<AnswerType>
{
    string questionString;
    AnswerType answer;
    public string QuestionString { get { return questionString; } }
    public AnswerType Answer { get { return answer; } }
    public Question(string qStr, AnswerType ans)
    {
        questionString = qStr;
        answer = ans;
    }
}