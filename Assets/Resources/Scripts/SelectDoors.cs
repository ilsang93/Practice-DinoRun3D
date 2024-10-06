using TMPro;
using UnityEngine;

public class SelectDoors : MonoBehaviour
{

    public SpriteRenderer rightDoorRD;
    public SpriteRenderer leftDoorRD;
    public TextMeshPro rightDoorText;
    public TextMeshPro leftDoorText;

    [SerializeField] private DoorType rightDoorType;
    public int rightDoorNum;
    [SerializeField] private DoorType leftDoorType;
    public int leftDoorNum;

    public Color goodColor;
    public Color badColor;

    private bool isExcuted = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateDoors();
    }

    public void CreateDoors()
    {
        rightDoorNum = Random.Range(1, 10);
        leftDoorNum = Random.Range(1, 10);

        // 우측이 Positive Effect
        if (Random.Range(0, 2f) > 1)
        {
            rightDoorRD.color = goodColor;
            // Plus Door
            if (Random.Range(0, 2f) > 1)
            {
                rightDoorType = DoorType.Plus;
                rightDoorText.text = "+" + rightDoorNum;
            }
            // Multiply Door
            else
            {
                rightDoorType = DoorType.Times;
                rightDoorText.text = "×" + rightDoorNum;
            }

            leftDoorRD.color = badColor;
            // Minus Door
            if (Random.Range(0, 2f) > 1)
            {
                leftDoorType = DoorType.Minus;
                leftDoorText.text = "-" + leftDoorNum;
            }
            // Division Door
            else
            {
                leftDoorType = DoorType.Division;
                leftDoorText.text = "÷" + leftDoorNum;
            }
        }
        else
        // 좌측이 Positive Effect
        {
            rightDoorRD.color = badColor;
            // Minus Door
            if (Random.Range(0, 2f) > 1)
            {
                rightDoorType = DoorType.Minus;
                rightDoorText.text = "-" + rightDoorNum;
            }
            // Division Door
            else
            {
                rightDoorType = DoorType.Division;
                rightDoorText.text = "÷" + rightDoorNum;
            }
            leftDoorRD.color = goodColor;
            // Plus Door
            if (Random.Range(0, 2f) > 1)
            {
                leftDoorType = DoorType.Plus;
                leftDoorText.text = "+" + leftDoorNum;
            }
            // Multiply Door
            else
            {
                leftDoorType = DoorType.Times;
                leftDoorText.text = "×" + leftDoorNum;
            }
        }
    }

    public void Excute(DinoContoller dino, bool horizontalFlg)
    {
        if (isExcuted)
        {
            return;
        }
        isExcuted = true;

        print("감지");

        if (horizontalFlg)
        {
            DinoCalc(dino, rightDoorType, rightDoorNum);
        }
        else
        {
            DinoCalc(dino, leftDoorType, leftDoorNum);
        }
    }

    private void DinoCalc(DinoContoller dino, DoorType doorType, int num)
    {
        switch (doorType)
        {
            case DoorType.Plus:
                for (int i = 0; i < num; i++) dino.AddRaptor();
                break;
            case DoorType.Minus:
                for (int i = 0; i < num; i++) dino.RemoveRaptor();
                break;
            case DoorType.Times:
                dino.TimesRaptor(num);
                break;
            case DoorType.Division:
                dino.DivisionRaptor(num);
                break;
        }
    }

    public enum DoorType
    {
        Plus,
        Minus,
        Times,
        Division
    }
}
