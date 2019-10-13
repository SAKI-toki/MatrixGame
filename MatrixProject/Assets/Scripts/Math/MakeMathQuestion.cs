using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 算数の問題を作成
/// </summary>
public class MakeMathQuestion : MakeQuestion<int>
{
    enum QuestionType { Add, Sub, Mul, Dev, None };

    public override Question<int> Make(List<int> answerList)
    {
        //答え
        int answer = answerList[Random.Range(0, answerList.Count)];
        //0か1の時
        if (answer == 0 || answer == 1)
        {
            return new Question<int>(answer.ToString() + (Random.Range(0, 2) == 0 ? " - 0" : " + 0"), answer);
        }
        //問題
        string questionString = "";
        switch ((QuestionType)System.Enum.GetValues(typeof(QuestionType)).GetValue(Random.Range(0, (int)QuestionType.None)))
        {
            //足し算
            case QuestionType.Add:
                {
                    int lhs = answer - Random.Range(1, answer);
                    int rhs = answer - lhs;
                    questionString = lhs.ToString() + " + " + rhs.ToString();
                }
                break;
            //引き算
            case QuestionType.Sub:
                {
                    int lhs = Random.Range(answer + 1, answer * 2);
                    int rhs = lhs - answer;
                    questionString = lhs.ToString() + " - " + rhs.ToString();
                }
                break;
            //掛け算
            case QuestionType.Mul:
                {
                    int rhs;
                    if (answer % 2 == 0 && answer % 3 == 0)
                        rhs = Random.Range(2, 4);
                    else if (answer % 2 == 0)
                        rhs = 2;
                    else if (answer % 3 == 0)
                        rhs = 3;
                    else
                        rhs = 1;

                    int lhs = answer / rhs;
                    questionString = lhs.ToString() + " × " + rhs.ToString();
                }
                break;
            //割り算
            case QuestionType.Dev:
                {
                    int rhs;
                    if (answer % 2 == 0 && answer % 3 == 0)
                        rhs = Random.Range(2, 4);
                    else if (answer % 2 == 0)
                        rhs = 2;
                    else if (answer % 3 == 0)
                        rhs = 3;
                    else
                        rhs = 1;

                    int lhs = answer * rhs;
                    questionString = lhs.ToString() + " ÷ " + rhs.ToString();
                }
                break;
        }
        return new Question<int>(questionString, answer);
    }
}