namespace AssetManager
{
    using System;
    using UnityEngine;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEditor;
    using System.IO;
    using System.Text;

    [Serializable]
    public class ModelImportSetting : AssetOption
    {
        [ShowInInspector, ReadOnly, PropertyOrder(-1)]
        public override string Path
        {
            get
            {
                return base.Path;
            }

            set
            {
                base.Path = value;
            }
        }
        bool AddCollider = false;
        bool KeepQuads = false;

        [FoldoutGroup("导入设置")]
        public ModelImporterMeshCompression MeshCompress = ModelImporterMeshCompression.Off;
        [FoldoutGroup("导入设置")]
        public bool IsReadable = false;
        [FoldoutGroup("导入设置")]
        public bool OptimizeMesh = true;
        [FoldoutGroup("导入设置")]
        public bool ImportBlendShapes = false;
        [FoldoutGroup("导入设置")]
        public bool GenerateLightMapUV = false;
        [FoldoutGroup("导入设置")]
        public ModelImporterNormals ImportNormals = ModelImporterNormals.Import;
        [FoldoutGroup("导入设置")]
        public ModelImporterTangents ImportTangents = ModelImporterTangents.CalculateMikk;
        [FoldoutGroup("导入设置")]
        public bool ImportMaterials = true;
        [FoldoutGroup("导入设置")]
        public ModelImporterMaterialName MaterialName = ModelImporterMaterialName.BasedOnTextureName;
        [FoldoutGroup("导入设置")]
        public ModelImporterMaterialSearch MaterialSearch = ModelImporterMaterialSearch.RecursiveUp;

        [FoldoutGroup("导入设置")]
        public ModelImporterAnimationType AnimationType = ModelImporterAnimationType.Generic;
        [FoldoutGroup("导入设置")]
        public bool ImportAnimation = true;

        [Serializable]
        class AssetInfo
        {
            [TableColumnWidth(280), ReadOnly]
            public string FileName;
            [TableColumnWidth(90), ReadOnly]
            public bool IsReadable;
            [TableColumnWidth(90), ReadOnly]
            public bool OptimizeMesh;
            [TableColumnWidth(90), ReadOnly]
            public bool ImpShapes;
            [TableColumnWidth(90), ReadOnly]
            public bool LightMapUV;
            [TableColumnWidth(90), ReadOnly]
            public bool ImpMaterials;
            [TableColumnWidth(90), ReadOnly]
            public bool ImpAnimation;
            [TableColumnWidth(100), ReadOnly]
            public ModelImporterAnimationType AnimationType;
            [ReadOnly]
            public string Other;
        }

        /// <summary>
        /// 按照配置重新导入资源
        /// </summary>
        public override void ReimportAsset()
        {
            base.ReimportAsset();

            ClearAsset();
            List<string> files = new List<string>(Directory.GetFiles(Path, "*.fbx", SearchOption.AllDirectories));
            for (int i = 0; i < files.Count; i++)
            {
                string refPath = files[i].Replace(Application.dataPath, "Assets").Replace("\\", "/");
                ModelImporter importer = ModelImporter.GetAtPath(refPath) as ModelImporter;
                ModelImporterMeshCompression meshCompress = importer.meshCompression;
                importer.meshCompression = MeshCompress;
                importer.isReadable = IsReadable;
                importer.optimizeMesh = OptimizeMesh;
                importer.importBlendShapes = ImportBlendShapes;
                importer.generateSecondaryUV = GenerateLightMapUV;
                importer.importNormals = ImportNormals;
                importer.importTangents = ImportTangents;
                importer.importMaterials = ImportMaterials;
                importer.materialName = MaterialName;
                importer.materialSearch = MaterialSearch;
                importer.animationType = AnimationType;
                importer.importAnimation = ImportAnimation;
                importer.addCollider = AddCollider;

                SerializedObject serialized = new SerializedObject(importer);
                var keepQuade = serialized.FindProperty("keepQuads");
                keepQuade.boolValue = KeepQuads;
                serialized.ApplyModifiedProperties();

                importer.SaveAndReimport();
                EditorUtility.DisplayProgressBar("按配置导入模型", refPath, (i + 1f) / files.Count);
            }
            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
    }
}