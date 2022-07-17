using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingCharactersManager
{
    [SerializeField] private MovingCharacter[] movingCharacters;

    [SerializeField] private float charactersSpeed;

    public void InitializeAllMovingCharacters(){
        for(int i = 0;i<movingCharacters.Length;i++){

            if(movingCharacters[i].coroutine != null){
                GameManager.instance.StopCoroutine(movingCharacters[i].coroutine);
            }
            movingCharacters[i].coroutine = GameManager.instance.StartCoroutine(MovingCharacterRoutine(i));
        }
    }

    public MovingCharacter GetMovingCharacter(string characterName){
        foreach(MovingCharacter mov in movingCharacters){
            if(mov.charName.Equals(characterName)){
                return mov;
            }
        }
        return null;
    }

    public string[] GetNPCNames(){
        string[] res = new string[movingCharacters.Length];
        for(int i = 0;i < res.Length;i++){
            res[i] = movingCharacters[i].charName;
        }
        return res;
    }

    IEnumerator MovingCharacterRoutine(int charIndex){
        MovingCharacter character = movingCharacters[charIndex];
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("CharactersPaths/"+character.charName+"/0"));
        bool infiniteLoop = false;
        int nbLoopsToDo = 0;
        string[] parameters;
        int lastStartLoop = 0;
        
        for(int i = 0;i < fileContent.Count;i++){
            string[] line = fileContent[i].Replace("\t","").Split("(");
            if(line.Length >= 2){
                line[1] = line[1].Split(")")[0];
            }
            switch(line[0]){
                case "MAP":
                    character.map = line[1];
                    break;
                case "TELEPORT":
                    parameters = line[1].Split(",");
                    character.placement = new Vector3(
                        float.Parse(parameters[0],System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(parameters[1],System.Globalization.CultureInfo.InvariantCulture),
                        character.placement.z);
                    break;
                case "WAIT":
                    character.moving = false;
                    yield return new WaitForSeconds(float.Parse(line[1],System.Globalization.CultureInfo.InvariantCulture));
                    break;
                case "WALK":
                    character.moving = true;
                    parameters = line[1].Split(",");
                    Vector3 target = new Vector3(
                        float.Parse(parameters[0],System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(parameters[1],System.Globalization.CultureInfo.InvariantCulture),
                        character.placement.z);

                    if(target.x == character.placement.x){
                        character.faceX = 0;
                    }else{
                        character.faceX = (target.x < character.placement.x) ? -1 : 1;
                    }

                    if(target.y == character.placement.y){
                        character.faceY = 0;
                    }else{
                        character.faceY = (target.y < character.placement.y) ? -1 : 1;
                    }

                    while(!target.Equals(character.placement)){
                        character.placement = Vector3.MoveTowards(character.placement,target,Time.deltaTime*charactersSpeed);
                        yield return new WaitForEndOfFrame();
                    }
                    break;
                case "{":
                    lastStartLoop = i;
                    break;
                case "}":
                    if(nbLoopsToDo != 0){
                        nbLoopsToDo--;
                    }
                    if(infiniteLoop || nbLoopsToDo != 0){
                        i = lastStartLoop;
                    }
                    break;
                case "DO":
                    if(line[1].Equals("INFINITE")){
                        infiniteLoop = true;
                    }else{
                        nbLoopsToDo = int.Parse(line[1]);
                    }
                    break;
                case "LOOK":
                    switch(line[1]){
                        case "EAST":
                            character.faceX = 1;
                            character.faceY = 0;
                            break;
                        case "WEST":
                            character.faceX = -1;
                            character.faceY = 0;
                            break;
                        case "SOUTH":
                            character.faceX = 0;
                            character.faceY = 1;
                            break;
                        case "NORTH":
                            character.faceX = 0;
                            character.faceY = -1;
                            break;
                    }
                    break;
                default:
                    Debug.Log(fileContent[i]);
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
