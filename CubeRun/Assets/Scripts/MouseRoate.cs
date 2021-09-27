using UnityEngine;

public class MouseRoate : MonoBehaviour
{
	private bool onDrag = false;  //�Ƿ���ק    
	private float speed = 6f;      //��ת�ٶ�    
	private float tempSpeed;      //�����ٶ� 
	private float axisX = 1;      //�����ˮƽ�����ƶ�������   
	private float axisY = 1;      //�������ֱ�����ƶ�������   
	private float cXY;
	private bool isRot=true;
	void OnMouseDown()
	{
		isRot = true;
		//������갴�µ��¼�// 
		axisX = 0f; axisY = 0f;
	}
	void OnMouseDrag()     //�����קʱ�Ĳ���// 
	{
		onDrag = true;
		axisX = -Input.GetAxis("Mouse X");
		//Debug.Log("Input.GetAxis(XXX)" + Input.GetAxis("Mouse X"));
		//���������� 
		axisY = Input.GetAxis("Mouse Y");
		cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY); //��������ƶ��ĳ���//
		if (cXY == 0f) { cXY = 1f; }
	}
	//   private void OnMouseExit()
	//   {
	//	isRot = false;

	//}
	//   private void OnMouseEnter()
	//   {
	//	isRot = true;
	//}
	void OnMouseUp()
	{
		//������갴�µ��¼�// 
		isRot = false;
	}
	//���������ٶ�
	float Rigid()
	{
		if (onDrag)
		{
			tempSpeed = speed;
		}
		else
		{
			if (tempSpeed > 0)
			{
				//ͨ����������ƶ�����ʵ����קԽ���ٶȼ���Խ��
				tempSpeed -= speed * 2 * Time.deltaTime / cXY;
			}
			else
			{
				tempSpeed = 0;
			}
		}
		return tempSpeed;
	}

	void Update()
	{
        //������ǰ���֮ǰ����һֱ������ת
        if (isRot)
        {
			this.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World);
			
		}
	}
}
