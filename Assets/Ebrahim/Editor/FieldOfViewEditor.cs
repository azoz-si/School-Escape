using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position,Vector3.up,Vector3.forward,360,fov.radius);

        Vector3 ViewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angel/2 );
        Vector3 ViewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angel /2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position,fov.transform.position + ViewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position,fov.transform.position + ViewAngle02 * fov.radius);
    }
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Rad2Deg), 0, Mathf.Cos(angleInDegrees * Mathf.Rad2Deg));
    }
}
