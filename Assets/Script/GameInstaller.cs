//using Netologia.Necro;
//using Netologia.Necro.Managers;
//using Netologia.Necro.Settings;
//using UnityEngine;
//using Zenject;

//public class GameManager : MonoInstaller
//{
//    private Controls _controls;

//    [SerializeField]
//    private CellManager _cellManager;

//    [SerializeField]
//    private SceneController _controller;

//    [SerializeField, Space(15f)]
//    private CellPaletteSettings _cellPaletteSettings;

//    public override void InstallBindings()
//    {
//        _controls = new Controls();
//        _controls.Game.Enable();
//        Container.BindInstance(_controls.Game).AsSingle();

//        Container.BindInstance(_cellManager).AsSingle();
//        Container.BindInstance(_controller).AsSingle();

//        Container.BindInstance(_cellPaletteSettings).AsSingle();

//        //for test
//        _cellManager.OnCellClicked += CellManagerOnOnCellClicked;
//    }

//    private void CellManagerOnOnCellClicked(Cell obj)
//    {
//        obj.SetSelect(_cellPaletteSettings.SelectCell);
//    }

//    private void OnDestroy()
//    {
//        _controls.Dispose();
//    }
//}

using Zenject;

namespace Netologia.Necro.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Controls>().AsSingle();

        }
    }
}