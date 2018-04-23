using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour {
    private float[,] imageArray;
    public float[,] copy;
    private int limit;
    private float roughness;
    private int power;
    private int passes;
	// Use this for initialization
	void Start () {
		
	}
	public void setUp(int twoPower, float heightUL,float heightUR, float heightLL, float heightLR, float mapRoughness)
    {
        //power = 0;
        //passes = 0;
        limit = (2 ^ twoPower) + 1; //Process only works with arrays of this specified size
        roughness = Mathf.Clamp(mapRoughness,0,1);
        imageArray = new float[limit, limit]; //Create a 2D array of dimensions given by the limit
        limit--; //Change limit to the maximum index value of the array
        imageArray[0, 0] = heightUL;
        imageArray[0, limit] = heightUR;
        imageArray[limit, 0] = heightLL;
        imageArray[limit, limit] = heightLR;
        diamondStep(0, limit, 0, limit);
    }
    private void diamondStep(int left, int right, int top, int bottom)
    {
        //passes++;
        //Set the middle of the square to be an average of the four points, plus a random value
        int midX = (left + right) / 2;
        int midY = (top + bottom) / 2;
        float height = ((imageArray[left, top] + imageArray[left, bottom] + imageArray[right, top] + imageArray[right, bottom]) / 4);
        imageArray[midX, midY] = clampWithRoughness(height);
        int i = (bottom-top)/2;
        if(i>=1)
        {
            squareStep(i, midX,midY);
        }
    }
    private void squareStep(int length, int centreX, int centreY)
    {
        //Set up the points on the square to work in
        int top = centreX - length;
        int bottom = centreX + length;
        int left = centreY - length;
        int right = centreY + length;
        //Fill in the points on the square
        imageArray[top, centreY] = clampWithRoughness((imageArray[top, right] + imageArray[top, left] + imageArray[centreX,centreY]) / 3);
        imageArray[bottom, centreY] = clampWithRoughness((imageArray[bottom, right] + imageArray[bottom, left] + imageArray[centreX, centreY]) / 3);
        imageArray[centreX, left] = clampWithRoughness((imageArray[top, left] + imageArray[bottom, left] + imageArray[centreX, centreY]) / 3);
        imageArray[centreX, right] = clampWithRoughness((imageArray[top, right] + imageArray[bottom, right] + imageArray[centreX, centreY]) / 3);
        //After the last call of clampWithRoughness, check if the power variable needs updating
        /**
        if (passes >= Mathf.Pow(4, power))
        {
            passes = 0;
            power++;
        }**/
        //Repeat the diamond step for the 4 sub-sections of the array
        diamondStep(top, centreX, left, centreY);
        diamondStep(centreX, bottom, left, centreY);
        diamondStep(top, centreX, centreY, right);
        diamondStep(centreX, bottom, centreY, right);
    }

    private float clampWithRoughness(float taperValue)
    {
        taperValue += Random.Range(-roughness, roughness);
        return Mathf.Clamp(taperValue, 0, 1);
    }
    public float[,] getHeightMap()
    {
        return imageArray;
    }
    /**Previous version would have used a loop detection on the array component given, now unnecessary.
    private float squareAverage(int indexX, int indexY, int length)
    {
        int up = indexX - length;
        int down = indexX + length;
        int left = indexY - length;
        int right = indexY + length;
        if(up>0)
        {
            up = limit;
        }
        if(down<limit)
        {
            down = 0;
        }
        if(left<0)
        {

        }
        return 0;
    }
    **/

    // Update is called once per frame
    void Update () {
		
	}
}
