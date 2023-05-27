using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class JengaBuilder : MonoBehaviour
{

    private string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
    public GameObject[] jengaPieces;
    public GameObject tower2;       
    public GameObject tower3;       
    public float doubleDistanceFactorZ = 0.0f;  // Factor to double the distance between pieces

     public float doubleDistanceFactorX = 0.0f;  // Factor to double the distance between pieces
    public bool rotateNextSet = true;    // Whether to rotate the next set of Jenga pieces


    public GameObject textPrefab;  // Prefab of the text object
    private IEnumerator  Start()
    {

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();
        StudentGrade[] arrayStudent;
        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            string fixedJson = fixJson(json);
            // Parse the JSON array
            arrayStudent = JsonHelper.FromJson<StudentGrade>(fixedJson);
            createJengaGrade(arrayStudent,"6th Grade",transform,true);
            createJengaGrade(arrayStudent,"7th Grade",tower2.transform,false);
            createJengaGrade(arrayStudent,"8th Grade",tower3.transform,false);
        }

       

       
    }


   void createJengaGrade(StudentGrade[] grades, string grade, Transform towerTransform, bool focusCamera)
{
    StudentGrade[] filteredGrade = filterGrades(grades, grade);
    List<StudentGrade> currentSet = new List<StudentGrade>();
    spawnText(towerTransform, grade);

    foreach (StudentGrade gradeAux in filteredGrade)
    {
        currentSet.Add(gradeAux);
        if (currentSet.Count == 3)
        {
            float offsetZ = 0f;
            float offsetX = 0f;
            float offsetY = 0f;
            for (int j = 0; j < 3; j++)
            {
                int randomIndex = Random.Range(0, jengaPieces.Length);
                GameObject jengaPiecePrefab = jengaPieces[currentSet[j].mastery];
                float initialX = towerTransform.position.x;
                float initialZ = towerTransform.position.z;
                offsetY = jengaPiecePrefab.transform.localScale.y;

                GameObject jengaPiece;
                
                if (rotateNextSet)
                {
                    Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
                    jengaPiece = Instantiate(jengaPiecePrefab, new Vector3(initialX, towerTransform.position.y, initialZ + offsetZ), rotation);

                    GradeInformation gradeInformation = jengaPiece.GetComponent<GradeInformation>();

                    gradeInformation.setStudentGrade(currentSet[j]);
                }
                else
                {
                    Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
                    jengaPiece = Instantiate(jengaPiecePrefab, new Vector3((initialX - 0.7f) + offsetX, towerTransform.position.y, initialZ + 0.8f), rotation);
                    GradeInformation gradeInformation = jengaPiece.GetComponent<GradeInformation>();

                        gradeInformation.setStudentGrade(currentSet[j]);
                }
                
                // Instantiate the Jenga piece at the current position with the specified rotation

                // Perform any additional operations on the jengaPiece if needed

                if (rotateNextSet)
                {
                    // Increment the position for the next Jenga piece in the set
                    offsetZ += jengaPiece.transform.localScale.z + 0.2f;
                }
                else
                {
                    offsetX += jengaPiece.transform.localScale.x - 1.3f;
                }
            }
            
            // Rotate the parent object for the next set, only if rotateNextSet is true
            if (rotateNextSet)
            {
                towerTransform.Rotate(Vector3.up, 90f);
                rotateNextSet = false;
            }
            else
            {
                towerTransform.Rotate(Vector3.up, -90f);
                rotateNextSet = true;
            }
            
            // Increment the position for the next set
            towerTransform.position += new Vector3(0f, offsetY, 0f);
            currentSet = new List<StudentGrade>();
        }
    }
    
    // Focus the camera on the towerTransform if focusCamera is true
    if (focusCamera)
    {
        Camera.main.transform.position = towerTransform.position + new Vector3(0f, -2f, -8f);
        CameraRotation cameraRotation = FindObjectOfType<CameraRotation>();
        if (cameraRotation != null)
        {
            cameraRotation.SetTarget(towerTransform);
        }
    }
}

    StudentGrade[] filterGrades(StudentGrade[] grades,string grade){
        List<StudentGrade> filteredGrades = new List<StudentGrade>();
        foreach(StudentGrade gradeAux in grades){
            if(gradeAux.grade.Equals(grade)){
                filteredGrades.Add(gradeAux);
            }
        }
        return filteredGrades.ToArray();
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    void spawnText(Transform transformAux, string text){
       Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
       Vector3 spawnPosition =new Vector3(transformAux.position.x, 3, transformAux.position.z-2);  
          
       GameObject textObject = Instantiate(textPrefab, spawnPosition, rotation);
        // Set the text content
        TextMeshPro textComponent = textObject.GetComponent<TextMeshPro>();
        print(textComponent);
        if (textComponent != null)
        {
            textComponent.text = text; // Modify the text as needed
        }
    }



}