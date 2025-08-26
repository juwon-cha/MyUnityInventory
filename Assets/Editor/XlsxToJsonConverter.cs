using UnityEngine;
using UnityEditor;
using System.IO;
using System.Data; // System.Data 사용
using ExcelDataReader; // ExcelDataReader 사용
using System.Text;

public class XlsxToJsonConverter : EditorWindow
{
    private string xlsxFilePath = "Assets/Data/ItemDataTable.xlsx"; // 기본 XLSX 파일 경로
    private string jsonOutputPath = "Assets/Resources/Item/ItemData"; // JSON 파일 저장 경로

    [MenuItem("Tools/Item Data Converter (XLSX)")]
    public static void ShowWindow()
    {
        GetWindow<XlsxToJsonConverter>("XLSX Converter");
    }

    // 에디터 윈도우의 GUI를 그리는 함수
    private void OnGUI()
    {
        GUILayout.Label("XLSX to JSON Converter", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // XLSX 입력 파일 경로 설정
        EditorGUILayout.LabelField("Source XLSX File", EditorStyles.boldLabel);
        // 가로로 경로 필드와 버튼을 배치하기 위해 BeginHorizontal/EndHorizontal 사용
        EditorGUILayout.BeginHorizontal();
        xlsxFilePath = EditorGUILayout.TextField(xlsxFilePath);
        if (GUILayout.Button("Find", GUILayout.Width(50))) // 버튼 너비를 50으로 고정
        {
            // 파일 열기 패널 띄움
            string path = EditorUtility.OpenFilePanel("Select XLSX File", "Assets", "xlsx");
            if (path.Length != 0)
            {
                // 전체 경로를 유니티 프로젝트 상대 경로로 변환
                xlsxFilePath = path.Substring(Application.dataPath.Length - "Assets".Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // JSON 출력 파일 경로 설정
        EditorGUILayout.LabelField("Output JSON File", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        jsonOutputPath = EditorGUILayout.TextField(jsonOutputPath);
        if (GUILayout.Button("Save To", GUILayout.Width(60))) // 버튼 너비를 60으로 고정
        {
            // 파일 저장 패널을 띄움. 사용자가 경로와 파일명을 직접 지정
            string path = EditorUtility.SaveFilePanel("Save JSON File", "Assets/Resources", "ItemData", "json");
            if (path.Length != 0)
            {
                // 전체 경로를 유니티 프로젝트 상대 경로로 변환.
                jsonOutputPath = path.Substring(Application.dataPath.Length - "Assets".Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        // "Convert" 버튼을 누르면 변환 작업을 수행합니다.
        if (GUILayout.Button("Convert XLSX to JSON"))
        {
            Convert();
        }
    }

    private void Convert()
    {
        if (string.IsNullOrEmpty(xlsxFilePath) || !File.Exists(xlsxFilePath))
        {
            Debug.LogError("XLSX file not found at path: " + xlsxFilePath);
            return;
        }

        // 파일을 스트림으로 연다. using을 사용 -> 작업이 끝나면 스트림이 자동으로 닫힌다.
        using (var stream = File.Open(xlsxFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            // 스트림을 이용해 ExcelReader 생성
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // 전체 데이터를 DataSet으로 읽어옴
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                });

                // 첫 번째 워크시트(테이블)를 가져옴
                DataTable table = result.Tables[0];
                ItemDataTable dataTable = new ItemDataTable();

                // 첫 번째 행(헤더)은 건너뛰고 두 번째 행부터 데이터를 읽음
                for (int i = 1; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];

                    // 빈 행이 있으면 건너뜀
                    if (row[0] == null || string.IsNullOrWhiteSpace(row[0].ToString()))
                    {
                        continue;
                    }

                    try
                    {
                        ItemData item = new ItemData
                        {
                            // 각 셀의 데이터를 타입에 맞게 변환합니다.
                            item_id = int.Parse(row[0].ToString()),
                            item_name = row[1].ToString(),
                            attack_power = int.Parse(row[2].ToString()),
                            defense = int.Parse(row[3].ToString()),
                            health = int.Parse(row[4].ToString()),
                            critical = int.Parse(row[5].ToString())
                        };
                        dataTable.items.Add(item);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError($"Error parsing row {i + 1}: {e.Message}");
                    }
                }

                // JSON으로 직렬화하고 파일로 저장
                string json = JsonUtility.ToJson(dataTable, true);
                File.WriteAllText(jsonOutputPath, json, Encoding.UTF8);

                AssetDatabase.Refresh();
                Debug.Log($"Successfully converted {dataTable.items.Count} items from XLSX to JSON at {jsonOutputPath}");
                EditorUtility.DisplayDialog("Success", "XLSX to JSON conversion completed successfully!", "OK");
            }
        }
    }
}