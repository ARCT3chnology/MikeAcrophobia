using UnityEngine;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Unity.VisualScripting;

public class UiController : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] GameObject _ThreeLetterRoundPanel;
    [SerializeField] GameObject _WelcomePanel;
    [SerializeField] VotingMenu _VotingPanel;
    [SerializeField] GameObject _EliminatedPanel;
    [SerializeField] GameObject _waitingPanel;
    [SerializeField] RoundConfigurator _RoundConfigurator;
    [SerializeField] GameEndMenu _GameEndMenu;
    [SerializeField] FaceOffMenu _faceOffMenu;
    [SerializeField] GameTieMenu _gameTieMenu;
    [SerializeField] ChatHandler chatHandler;
    [SerializeField] GameObject SingleWaitingPanel;
    [SerializeField] PlayerLeftUI _playerLeftUI;
    private ExitGames.Client.Photon.Hashtable stats = new ExitGames.Client.Photon.Hashtable();

    public GameObject welcomePanel { get { return _WelcomePanel; } }
    public GameObject threeLetterRound { get { return _ThreeLetterRoundPanel; } }
    public GameEndMenu gameEndMenu { get { return _GameEndMenu; } }
    public VotingMenu votingPanel { get { return _VotingPanel; } }
    public GameObject eliminatedPanel { get { return _EliminatedPanel; } }
    public GameObject waitingPanel { get { return _waitingPanel; } }
    public RoundConfigurator roundConfigurator { get { return _RoundConfigurator; } }
    public FaceOffMenu faceOffMenu { get { return _faceOffMenu; } }
    public GameTieMenu GameTieMenu { get { return _gameTieMenu; } }
    public PlayerLeftUI PlayerLeftUI { get { return _playerLeftUI; } }
    private void Start()
    {
        //PhotonPeer.RegisterType(,);
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowWelcomePanel), RpcTarget.All);
        }
        AudioManager.Instance.Play("Welcome");
        AudioManager.Instance.Play("Gameplay");
        GameSettings.normalGame = true;
    }

    public void Start3LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting 3 letter round");
            photonView.RPC(nameof(RPC_ShowFirstRoundPanel), RpcTarget.AllBuffered);
        }
    }
    public void Start4LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting 4 letter round");
            photonView.RPC(nameof(RPC_ShowSecondRoundPanel), RpcTarget.AllBuffered);
        }
    }
    public void Start5LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting 5 letter round");
            photonView.RPC(nameof(RPC_ShowThirdRoundPanel), RpcTarget.AllBuffered);
        }
    }
    public void Start6LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowFourthRoundPanel), RpcTarget.AllBuffered);
        }
    }    
    public void Start7LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowFifthRoundPanel), RpcTarget.AllBuffered);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {

        OnPlayerVoted(propertiesThatChanged);

    }

    //When ever player votes this function is executed.
    private void OnPlayerVoted(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(GameSettings.PlAYERS_VOTED))
        {
            votingPanel.updateVotesStats(4, (int)propertiesThatChanged[GameSettings.PlAYERS_VOTED]);
            if (GameSettings.normalGame)
            {
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.TOURNAMENT_NUMBER] == 0)
                {
                    if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == PhotonNetwork.CurrentRoom.PlayerCount)
                    {
                        for (int j = 0; j < votingPanel.voteList.Count; j++)
                        {
                            //Debug.Log("P" + (j + 1).ToString() + "Votes");
                            if (j == 0)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                            }
                            if (j == 1)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES]);
                            }
                            if (j == 2)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES]);
                            }
                            if (j == 3)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES]);
                            }                            
                            if (j == 4)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER5_VOTES]);
                            }                            
                            if (j == 5)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER6_VOTES]);
                            }                            
                            if (j == 6)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER7_VOTES]);
                            }                            
                            if (j == 7)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER8_VOTES]);
                            }                            
                            if (j == 8)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER9_VOTES]);
                            }                            
                            if (j == 9)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER10_VOTES]);
                            }
                            
                        }
                        votingPanel.voteTimer.gameObject.SetActive(false);
                        onVotingTimeEnded();
                    }
                }
            else if ((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.TOURNAMENT_NUMBER] == 1)
            {
                if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == 3)
                {
                    for (int j = 0; j < votingPanel.voteList.Count; j++)
                    {
                        //Debug.Log("P" + (j + 1).ToString() + "Votes");
                        if (j == 0)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                        }
                        if (j == 1)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES]);
                        }
                        if (j == 2)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES]);
                        }
                        if (j == 3)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES]);
                        }
                        if (j == 4)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER5_VOTES]);
                        }                        
                        if (j == 5)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER6_VOTES]);
                        }                        
                        if (j == 6)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER7_VOTES]);
                        }                        
                        if (j == 7)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER8_VOTES]);
                        }                        
                        if (j == 8)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER9_VOTES]);
                        }                        
                        if (j == 9)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER10_VOTES]);
                        }
                    }
                    onVotingTimeEnded();
                }
            }
            }
            else
            {
                Debug.Log("Voters Count: " + faceOffVoters.Count);
                //Both the two players submitted their votes
                if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == faceOffVoters.Count)
                {
                    for (int i = 0; i < faceOffVoters.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                            {
                                StartCoroutine(showVotes());
                            }
                        
                        }

                        if(i == 1)
                        {
                            if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                            {
                                StartCoroutine(showVotes());
                            }

                        }
                    }
                    for (int i = 0; i < faceOffPlayers.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                            {
                                StartCoroutine(showFaceOffAfterVotesWaiting());
                            }
                        
                        }

                        if(i == 1)
                        {
                            if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                            {
                                StartCoroutine(showFaceOffAfterVotesWaiting());
                            }

                        }

                    }
                    if (!GameManager.faceOffRoundNumberIncreased)
                    {
                        //GameManager.faceOffRoundNumber++;
                        GameManager.updateFaceOffRoundNumber();
                        GameManager.faceOffRoundNumberIncreased = true;
                    }
                    Debug.Log("All two sumbitted their votes");       
                }
            }
        }
    }


    public void DisableFaceoffVoteMenuFromAll()
    {
        Debug.Log("DisableFaceoffVoteMenuFromAll");
        photonView.RPC(nameof(RPC_DisableFaceOffVoteMenu), RpcTarget.All);
        resetPlayerVotedCount();
    }

    [PunRPC]
    private void RPC_DisableFaceOffVoteMenu()
    {
        faceOffMenu.DisableVotingOption();
    }


    public IEnumerator showVotes()
    {
        for (int i = 0; i < faceOffVoters.Count; i++)
        {
            if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
            {
                Debug.Log("showing votes Of: " + faceOffPlayers[i].NickName);
                Debug.Log("showing votes Of: " + faceOffPlayers[i + 1].NickName);

                yield return new WaitForSeconds(.1f);
                for (int j = 0; j < faceOffPlayers.Count; j++)
                {
                    int votes = (int)faceOffPlayers[j].CustomProperties[GameSettings.PLAYER_VOTES];
                    faceOffMenu.showPlayerVotes(votes,j);
                }
                //int votes2 = (int)faceOffPlayers[i + 1].CustomProperties[GameSettings.PLAYER_VOTES];
                //faceOffMenu.showP2Votes(votes2);
            }

            //if (i == 0)
            //{
            //    if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
            //    {
            //        Debug.Log("showing votes Of: " + faceOffPlayers[i].NickName);
            //        Debug.Log("showing votes Of: " + faceOffPlayers[i + 1].NickName);

            //        yield return new WaitForSeconds(.1f); 
            //        int votes = (int)faceOffPlayers[i].CustomProperties[GameSettings.PLAYER_VOTES];
            //        int votes2 = (int)faceOffPlayers[i + 1].CustomProperties[GameSettings.PLAYER_VOTES];
            //        faceOffMenu.showPlayerVotes(votes);
            //        //faceOffMenu.showP2Votes(votes2);
            //    }

            //}

            //if (i == 1)
            //{
            //    if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
            //    {
            //        Debug.Log("showing votes Of: " + faceOffPlayers[i - 1].NickName);
            //        Debug.Log("showing votes Of: " + faceOffPlayers[i].NickName);

            //        yield return new WaitForSeconds(.1f);
            //        int votes1 = (int)faceOffPlayers[i - 1].CustomProperties[GameSettings.PLAYER_VOTES];
            //        int votes = (int)faceOffPlayers[i].CustomProperties[GameSettings.PLAYER_VOTES];
            //        faceOffMenu.showP1Votes(votes1);
            //        faceOffMenu.showP2Votes(votes);
            //    }

            //}
        }
        for (int i = 0; i < faceOffPlayers.Count; i++)
        {
            photonView.RPC(nameof(RPC_UpdateStarsUI), faceOffPlayers[i]);
        }
    }
    public IEnumerator showFaceOffAfterVotesWaiting()
    {
        for (int i = 0; i < faceOffPlayers.Count; i++)
        {
            if (i == 0)
            {
                if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i + 1].NickName);

                    yield return new WaitForSeconds(.1f);
                    faceOffMenu.setInfoPanelText("Please Wait -- All Player voted");
                }

            }

            if (i == 1)
            {
                if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i - 1].NickName);
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);

                    yield return new WaitForSeconds(.1f);
                    faceOffMenu.setInfoPanelText("Please Wait -- All Player voted");

                }

            }

        }
    }

    public void onVotingTimeEnded()
    {
        Debug.Log("onVotingTimeEnded");
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(GameSettings.PlAYERS_VOTED) != false)
        {
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED] < PhotonNetwork.CurrentRoom.PlayerCount)
            {
                votingPanel.updateVotesStats(PhotonNetwork.PlayerList.Count(), (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED]);
                votingPanel.hideAllVoteButton();
                for (int j = 0; j < votingPanel.voteList.Count; j++)
                {
                    //Debug.Log("P" + (j + 1).ToString() + "Votes");
                    if (j == 0)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                    }
                    if (j == 1)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES]);
                    }
                    if (j == 2)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES]);
                    }
                    if (j == 3)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES]);
                    }                    
                    if (j == 4)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER5_VOTES]);
                    }                    
                    if (j == 5)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER6_VOTES]);
                    }                    
                    if (j == 6)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER7_VOTES]);
                    }                    
                    if (j == 7)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER8_VOTES]);
                    }                    
                    if (j == 8)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER9_VOTES]);
                    }                    
                    if (j == 9)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER10_VOTES]);
                    }
                }
            }
        }
        if (PhotonNetwork.IsMasterClient && GameManager.getroundNumber() < 5)
        {
            GameManager.updateRoundNumber();
        }
        GameManager.updateAnswersSubmittedNumber(0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.VOTING_IN_PROGRESS, false } });
        resetPlayerAnswer();
        Invoke(nameof(StartNextRound), 5f);
    }


    /// <summary>
    /// For reseting player answers to null - for the next round.
    /// </summary>
    public void resetPlayerAnswer()
    {
        stats = new ExitGames.Client.Photon.Hashtable();
        stats[GameSettings.PlAYER_ANSWER] = "";
        PhotonNetwork.SetPlayerCustomProperties(stats);
    }

    public void StartNextRound()
    {
        if(threeLetterRound.activeInHierarchy)
        {
            Debug.Log("StartNextRound");        
            threeLetterRound.gameObject.SetActive(false);
            votingPanel.gameObject.SetActive(false);
            votingPanel.voteTimer.StartTime = false;
            votingPanel.resetVotesList();
            waitingPanel.SetActive(true);
            if (PhotonNetwork.IsMasterClient)
            {
                resetPlayerVotedCount();
            }
        }    
    }

    public void resetPlayerVotedCount()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, 0 } });
        //PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.NO_OF_ANSWERS_SUBMITTED, 0 } });
    }

    public void GameCompleted()
    {
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        
        int votes = 0, maxIndex = 0;
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        //checking for same values
        if(allVotes.ToList().Distinct().Count() == 1 && PhotonNetwork.PlayerList.Count() == allVotes.Count())
        {
            //it means that all the player contains the same number of votes.
            Debug.Log("all players get same votes.");
            Debug.Log("Game tied");
            GameTieMenu.gameObject.SetActive(true);
            GameTieMenu.showPlayers();
            waitingPanel.gameObject.SetActive(false);
            //GameTieMenu.showPlayers();
            GameSettings.PlayerInRoom = false;
            if (GameSettings.CurrentRooms != null)
            {
                foreach (var item in GameSettings.CurrentRooms)
                {
                    if (item.roomName == PhotonNetwork.CurrentRoom.Name)
                        item.playerCount = 0;
                }
            }
            //GameManager.updateRoundNumber(0);
            //Handled in waiting panel script - OnEnable.
        }
        else
        {
            int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
            Debug.Log("Max count is: " + maxCount);
            if (maxCount == 1)
            {
                AudioManager.Instance.Stop("Gameplay");
                votes = allVotes.Max();
                maxIndex = allVotes.ToList().IndexOf(votes);
                //gameEndMenu.StartTimer();
                PhotonNetwork.AutomaticallySyncScene = false;
                if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[maxIndex])
                {
                    //photonView.RPC(nameof(RPC_UpdateGamesWin), PhotonNetwork.PlayerList[maxIndex]);
                    RPC_UpdateGamesWin();
                }
                else
                {
                    RPC_UpdateGamesLost();
                }

                photonView.RPC(nameof(RPC_ShowLevelComplete), RpcTarget.All, PhotonNetwork.PlayerList[maxIndex].NickName, votes);
                Debug.Log("GameCompleted: " + maxIndex.ToString());
                GameSettings.PlayerInRoom = false;
                if (GameSettings.CurrentRooms != null)
                {
                    foreach (var item in GameSettings.CurrentRooms)
                    {
                        if(item.roomName == PhotonNetwork.CurrentRoom.Name)
                            item.playerCount = 0;
                    }
                }

                //gameEndMenu.setEndPanelStats(PhotonNetwork.PlayerList[maxIndex].NickName,votes);
            }
            else if(maxCount == PhotonNetwork.PlayerList.Count())
            {
                //if all the players in the room get the same votes then.
                //show tie panel.
                Debug.Log("Game tied");
                GameTieMenu.gameObject.SetActive(true);
                GameTieMenu.showPlayers();
                //GameTieMenu.showPlayers();
                GameSettings.PlayerInRoom = false;
                if (GameSettings.CurrentRooms != null)
                {
                    foreach (var item in GameSettings.CurrentRooms)
                    {
                        if (item.roomName == PhotonNetwork.CurrentRoom.Name)
                            item.playerCount = 0;
                    }
                }
            }
            else if (maxCount >= 2)
            {
                if (GameManager.getFaceOffRoundNumber() < 3 )
                {
                    FaceOffRounds();
                }
                //onthreePlayerGotSameVotes();
            }
        }
    }

    [PunRPC]
    private void RPC_UpdateGamesLost()
    {
        PlayerStatsMenu.Instance.UpdateMatchesLost();
    }
    [PunRPC]
    private void RPC_UpdateGamesWin()
    {
        PlayerStatsMenu.Instance.UpdateMatchesWon();
    }

    //[PunRPC]
    //public void RPC_GameCompleted() 
    //{
    //    GameCompleted();
    //}

    [PunRPC]
    public void RPC_ShowLevelComplete(string nickname, int votes)
    {
        gameEndMenu.gameObject.SetActive(true);
        gameEndMenu.setEndPanelStats(nickname, votes);
        //PhotonNetwork.Disconnect();
    }

    public void onthreePlayerGotSameVotes()
    {
        Debug.Log("3 persons got same votes.");
        //remove the one with the lowest score from the game
        //and start new game with the remaining three.
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int minimumValueIndex = allVotes.ToList().IndexOf(allVotes.Min());
        Debug.Log("Player with the lowest vote is: " + PhotonNetwork.PlayerList[minimumValueIndex].NickName);
        if (PhotonNetwork.PlayerList[minimumValueIndex] == PhotonNetwork.LocalPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                DisconnectPlayer();
            }
            GameSettings.PlayerInRoom = false;
        }

    }

    public void DisconnectPlayer()
    {
        AudioManager.Instance.Play("MenuButton");
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.CloseConnection(PhotonNetwork.LocalPlayer);
        if(PhotonNetwork.NetworkClientState != ClientState.Disconnected)
        {
            PhotonNetwork.LeaveRoom();
            while (PhotonNetwork.InRoom)
            {
                yield return null;
            }
            AudioManager.Instance.Stop("Gameplay");
            PhotonNetwork.Disconnect(); // Disconnect from Photon network
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void loadLobby()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (PhotonNetwork.PlayerList[i]!= PhotonNetwork.MasterClient)
            {
                photonView.RPC(nameof(RPC_LeaveRoom), RpcTarget.All, PhotonNetwork.PlayerList[i]);
            }
                //RPC_LeaveRoom(PhotonNetwork.PlayerList[i]);
        }
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(DisconnectAndLoad());
        }
        //photonView.RPC(nameof(RPC_LeaveRoom), RpcTarget.All);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined in Gameplay:");
        Debug.Log("Voting in Progress: " + GameManager.isVotingInprogress());
        GameSettings.PlayerInRoom = true;
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            chatHandler.JoinRoomChat(PhotonNetwork.CurrentRoom.Name);
        }

        if (GameManager.getFaceOffInProgress())
        {
            GameSettings.normalGame = false;
        }

        if (!GameSettings.normalGame)
        {
            StartCoroutine(startFaceOffForOthers());
        }

        if (GameManager.isVotingInprogress())
        {
            SingleWaitingPanel.SetActive(true);
            roundConfigurator.roundTimer.StartTime = false;
        }
        else
        {
            SingleWaitingPanel.SetActive(false);
            Debug.Log("Voting Is Over");
        }
        //base.OnJoinedRoom();
    }

    public IEnumerator startFaceOffForOthers()
    {
        yield return new WaitForSeconds(0);
        Debug.Log("Da face off round is in progress");
        Debug.Log("Voters: " + faceOffVoters.Count);
        Debug.Log("Players: " + faceOffPlayers.Count);
        photonView.RPC(nameof(RPC_AddFaceOffVoters), RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer);
        RPC_faceOffVoter();
    }


    [PunRPC]
    public void RPC_LeaveRoom(Player p)
    {
        PhotonNetwork.CloseConnection(p);
        SceneManager.LoadScene(1);
        Debug.Log("leaving Room");
        //PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene(1);
    }

    // Call this method to switch the master client
    public void SwitchMasterClient(Player newMasterClient)
    {
        // Make sure you are the current master client before switching
        if (PhotonNetwork.IsMasterClient)
        {
            // Set the new master client
            PhotonNetwork.SetMasterClient(newMasterClient);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("Master Client Changed to :" + newMasterClient.NickName);

        base.OnMasterClientSwitched(newMasterClient);
    }

    //public int playerleft;
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //SwitchMasterClient(PhotonNetwork.PlayerList[0]);
        PlayerLeftUI.showText(otherPlayer.NickName);
        //playerleft = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_LEFT];
        //playerleft++;
        //PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_LEFT,playerleft} });
        if (votingPanel.gameObject.activeSelf)
        {
            votingPanel.removePlayerFromVoteList(otherPlayer);
        }
        if (GameManager.getFaceOffInProgress())
        {
            photonView.RPC(nameof(RPC_removeFaceoffPlayer), RpcTarget.AllBuffered, otherPlayer);
            photonView.RPC(nameof(RPC_removeFaceoffVoter), RpcTarget.AllBuffered, otherPlayer);

        }


        Debug.Log(otherPlayer.NickName + " Left the Room");
        if(GameManager.getroundNumber()==5 && GameManager.threePlayerGotSameVotes())
        {
            if (otherPlayer.NickName != PhotonNetwork.LocalPlayer.NickName)
            {
                Debug.Log("starting next round");
                StartCoroutine(startNextRound(otherPlayer));
            }
            else
            {
                GameSettings.ConnectedtoMaster = false;
                SceneManager.LoadScene(1);
            }
        }

        if(PhotonNetwork.CurrentRoom.PlayerCount == 0)
            PhotonNetwork.CurrentRoom.IsOpen = true;


        //if(PhotonNetwork.LocalPlayer == otherPlayer)
        //{
        //    SceneManager.LoadScene(1);
        //}
        base.OnPlayerLeftRoom(otherPlayer);
    }

    //public override onmas
    [PunRPC]
    public void RPC_removeFaceoffPlayer(Player playerToRemove)
    {
        for (int i = 0; i < faceOffPlayers.Count; i++)
        {
            if (playerToRemove == faceOffPlayers[i])
            {
                Debug.Log("Removing faceoff player");
                faceOffPlayers.RemoveAt(i);
                faceOffMenu.removePlayerFromVoter();
            }
        }

        if (faceOffPlayers.Count() == 1)
        {
            //All Players left the Last is the Winner.
            //GameCompleted();
            GameCompletedForFaceOff();
        }
    }

    //This function is called when all the players of the face-off rounds left the room except the one who is also the
    //player then he is gonna win.
    public void GameCompletedForFaceOff()
    {
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];

        int votes = 0, maxIndex = 0;
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        AudioManager.Instance.Stop("Gameplay");
        votes = allVotes.Max();
        //maxIndex = allVotes.ToList().IndexOf(votes);
        //gameEndMenu.StartTimer();
        PhotonNetwork.AutomaticallySyncScene = false;
        if (PhotonNetwork.LocalPlayer == faceOffPlayers[0])
        {
            //photonView.RPC(nameof(RPC_UpdateGamesWin), PhotonNetwork.PlayerList[maxIndex]);
            RPC_UpdateGamesWin();
        }
        else
        {
            RPC_UpdateGamesLost();
        }

        photonView.RPC(nameof(RPC_ShowLevelComplete), RpcTarget.All, faceOffPlayers[0].NickName, votes);
        Debug.Log("GameCompleted: " + maxIndex.ToString());
        GameSettings.PlayerInRoom = false;
        if (GameSettings.CurrentRooms != null)
        {
            foreach (var item in GameSettings.CurrentRooms)
            {
                if (item.roomName == PhotonNetwork.CurrentRoom.Name)
                    item.playerCount = 0;
            }
        }

        //gameEndMenu.setEndPanelStats(PhotonNetwork.PlayerList[maxIndex].NickName,votes);
    }
    [PunRPC]
    public void RPC_removeFaceoffVoter(Player playerToRemove)
    {
        for (int i = 0; i < faceOffVoters.Count; i++)
        {
            if (playerToRemove == faceOffVoters[i])
            {
                Debug.Log("Removing faceoff Voter");
                faceOffVoters.RemoveAt(i);
            }
        }
    }

    public IEnumerator startNextRound(Player otherPlayer)
    {
        Debug.Log("Round Starting: "+ GameManager.getroundNumber());
        waitingPanel.GetComponent<WaitingPanel>().SetText(otherPlayer.NickName + " is kicked");
        yield return new WaitForSeconds(3);
        waitingPanel.SetActive(false);
        GameManager.updateRoundNumber(0);
        GameManager.updateTournamentNumber(1);
        yield return new WaitForSeconds(1);
        welcomePanel.SetActive(true);
        //waitingPanel.GetComponent<WaitingPanel>().StartGame();
    }


    public List<Player> faceOffPlayers 
    {
        get;
        set;
    }

    public List<Player> faceOffVoters 
    {
        get;
        set;
    }

    

    public void FaceOffRounds()
    {
        faceOffPlayers = new List<Player>();
        faceOffVoters = new List<Player>();
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int MaxNoOfVotes = allVotes.Max();
        Debug.Log("More than 1 have same votes. Its time to start the face off round.");
        var duplicatesWithIndices = allVotes
        // Associate each name/value with an index
        .Select((Name, Index) => new { Name, Index })
        // Group according to name
        .GroupBy(x => x.Name)
        // Only care about Name -> {Index1, Index2, ..}
        .Select(xg => new
        {
            Name = xg.Key,
            Indices = xg.Select(x => x.Index)
        })
        .OrderByDescending(x => x.Name)
        // And groups with more than one index represent a duplicate key
        .Where(x => x.Indices.Count() > 1);
        foreach (var g in duplicatesWithIndices)
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    for (int i = 0; i < g.Indices.ToArray().Count(); i++)
            //    {
            //        if (g.Name == allVotes.Max())
            //        {
            //            photonView.RPC(nameof(RPC_AddFaceOffPlayer), RpcTarget.AllBuffered, PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
            //            //faceOffPlayers.Add(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
            //            //Debug.Log("FaceOff Players: " + PhotonNetwork.PlayerList[g.Indices.ToArray()[i]].NickName);
            //            startFaceOffRound(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
            //        }
            //    }
            //}
            //else
            for (int i = 0; i < g.Indices.ToArray().Count(); i++)
            {
                if (g.Name == allVotes.Max())
                {
                    if(PhotonNetwork.IsMasterClient)
                        photonView.RPC(nameof(RPC_AddFaceOffPlayer), RpcTarget.AllBuffered, PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                    
                    //faceOffPlayers.Add(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                    Debug.Log("FaceOff Players: " + PhotonNetwork.PlayerList[g.Indices.ToArray()[i]].NickName);
                    startFaceOffRound(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                }
            }

            //Debug.Log("Have duplicate " + g.Name + " with indices " +
            //    string.Join(",", g.Indices.ToArray()));
        }
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    for (int i = 0; i < allVotes.Length; i++)
        //    {
        //        if (allVotes[i] != MaxNoOfVotes)
        //        {
        //            photonView.RPC(nameof(RPC_AddFaceOffVoters), RpcTarget.AllBuffered, PhotonNetwork.PlayerList[i]);
        //            //faceOffVoters.Add(PhotonNetwork.PlayerList[i]);
        //            //Debug.Log("FaceOff Voters: " + PhotonNetwork.PlayerList[i].NickName);
        //            startFaceOffVoter(PhotonNetwork.PlayerList[i]);
        //        }
        //    }
        //}

        for (int i = 0; i < allVotes.Length; i++)
        {
            if (allVotes[i] != MaxNoOfVotes)
            {
                if(PhotonNetwork.IsMasterClient)
                    photonView.RPC(nameof(RPC_AddFaceOffVoters), RpcTarget.AllBuffered, PhotonNetwork.PlayerList[i]);
                //faceOffVoters.Add(PhotonNetwork.PlayerList[i]);
                Debug.Log("FaceOff Voters: " + PhotonNetwork.PlayerList[i].NickName);
                startFaceOffVoter(PhotonNetwork.PlayerList[i]);
            }
        }

        //PhotonNetwork.CleanRpcBufferIfMine(photonView);
        //PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
        //changing room stats to notify other players that faceoff rounds are started.
        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.setFaceOffInProgress(true);
        }
    }



    [PunRPC]
    private void RPC_AddFaceOffPlayer(Player playerToAdd)
    {
        if(faceOffPlayers == null)
        {
            faceOffPlayers = new List<Player>();
            faceOffPlayers.Add(playerToAdd);
        }
        else
        {
            faceOffPlayers.Add(playerToAdd);
        }
        Debug.Log("Adding RPC_FACEOFF Players: " + faceOffPlayers.Count);

    }
    [PunRPC]
    private void RPC_AddFaceOffVoters(Player playerToAdd)
    {
        if(faceOffVoters == null)
        {
            faceOffVoters = new List<Player>();
            faceOffVoters.Add(playerToAdd);
        }
        else
        {
            faceOffVoters.Add(playerToAdd);
        }
        Debug.Log("Adding RPC_FACEOFF VOTERS: " + faceOffVoters.Count);
    } 

    public void restartGame()
    {
        waitingPanel.SetActive(false);
        welcomePanel.SetActive(true);
    }

    public void startFaceOffRound(Player p)
    {
        Debug.Log("Starting face off round");
        GameSettings.FaceOffGame = true;
        GameSettings.normalGame = false;
        photonView.RPC(nameof(RPC_ShowWaitingPanel), p);
    }

    public void startFaceOffVoter(Player p)
    {
        GameSettings.FaceOffGame = true;
        GameSettings.normalGame = false;
        photonView.RPC(nameof(RPC_ShowWaitingPanel), p);
    }

    public void turnOffTextPanel(Player p)
    {
        photonView.RPC(nameof(RPC_TurnOFFTextPanel), p);
    }

    public void turnOffTextPanel( bool startVotingTime)
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        //threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        votingPanel.gameObject.SetActive(true);
        
        if(startVotingTime)
            votingPanel.voteTimer.StartTimer();
        
    }
    public void RPC_OnFaceOffAnswerSubmit(Player p)
    {
        Debug.Log("FaceOff - answer Submit Name: " + p.NickName);
        photonView.RPC(nameof(turnOffTextPanelFaceOff_Voter1), p);
        //photonView.RPC(nameof(turnOffTextPanelFaceOff_Voter2), p);
    }


    [PunRPC]
    private void turnOffTextPanelFaceOff_Voter1()
    {
        Debug.Log("FaceOff - Turning off text panel: " );
        faceOffMenu.onAnswerSubmission();
        faceOffMenu.setVoteButtonInteractableState(true);
        faceOffMenu.setVoteButtonState(true);

        for (int i = 0; i < faceOffVoters.Count(); i++)
        {
            for (int j = 0; j < faceOffPlayers.Count(); j++)
            {
                Debug.Log("FaceOff - Turning off text panel: For Player: " + (string)faceOffPlayers[j].CustomProperties[GameSettings.PlAYER_ANSWER]);
                //photonView.RPC(nameof(RPC_ShowFaceOffPlayerAnswer), faceOffVoters[i], (string)faceOffPlayers[j].CustomProperties[GameSettings.PlAYER_ANSWER],i);
                RPC_ShowFaceOffPlayerAnswer((string)faceOffPlayers[j].CustomProperties[GameSettings.PlAYER_ANSWER],j);
            }
            //photonView.RPC(nameof(RPC_ShowFaceOffP2Answer), faceOffVoters[i], (string)faceOffPlayers[1].CustomProperties[GameSettings.PlAYER_ANSWER]);
            photonView.RPC(nameof(RPC_StartFaceOffVotingTimer), faceOffVoters[i]);
        }

        faceOffMenu.Vote_Timer.StartTimer();
        //votingPanel.gameObject.SetActive(true);
    }
    //[PunRPC]
    //private void turnOffTextPanelFaceOff_Voter2()
    //{
    //    faceOffMenu.onAnswerSubmission();
    //    faceOffMenu.setVoteButtonInteractableState(true);
    //    faceOffMenu.setVoteButtonState(true);
    //    photonView.RPC(nameof(RPC_ShowFaceOffPlayerAnswer), faceOffVoters[1], (string)faceOffPlayers[0].CustomProperties[GameSettings.PlAYER_ANSWER]);
    //    //photonView.RPC(nameof(RPC_ShowFaceOffP2Answer), faceOffVoters[1], (string)faceOffPlayers[1].CustomProperties[GameSettings.PlAYER_ANSWER]);
    //    photonView.RPC(nameof(RPC_StartFaceOffVotingTimer), faceOffVoters[1]);
    //    faceOffMenu.Vote_Timer.StartTimer();
    //    //votingPanel.gameObject.SetActive(true);
    //}
    public void makePlayerWaitForFaceOffVoting(Player P)
    {
        photonView.RPC(nameof(RPC_MakePlayerWaitinFaceOff), P);
    }
    [PunRPC]
    private void RPC_MakePlayerWaitinFaceOff()
    {
        faceOffMenu.showWaitingForVoting();
        faceOffMenu.hidePlayerPanel();
    }
    [SerializeField] string acroText;
    [HideInInspector] char[] alphabets = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public int[] returnRandomAlphabet()
    {
        int a = UnityEngine.Random.Range(0, 26);
        int b = UnityEngine.Random.Range(0, 26);
        int c = UnityEngine.Random.Range(0, 26);
        int d = UnityEngine.Random.Range(0, 26);
        int e = UnityEngine.Random.Range(0, 26);
        int f = UnityEngine.Random.Range(0, 26);
        int g = UnityEngine.Random.Range(0, 26);
        int[] alphas = new int[] { a, b, c, d, e, f, g };
        return alphas;
    }

    public void StartFaceOffRounds()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            switch (GameManager.getFaceOffRoundNumber())
            {

                case 0:
                    {
                        AcroForFaceOff(AcronymSetter.acronyms.ThreeLetters);

                        break;
                    }
                case 1:
                    {
                        AcroForFaceOff(AcronymSetter.acronyms.FourLetters);

                        break;
                    }
                case 2:
                    {
                        AcroForFaceOff(AcronymSetter.acronyms.FiveLetters);

                        break;
                    }
                default:
                    break;
            }
            Debug.Log("Mater Setting Acronym: " + acroText);
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.ACRO_FOR_FACEOFF, acroText } });
        }

        for (int i = 0; i < faceOffPlayers.Count; i++)
        {
            if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
            {
                photonView.RPC(nameof(RPC_faceOffPlayer), faceOffPlayers[i]);
            }
        }

        for (int i = 0; i < faceOffVoters.Count; i++)
        {
            if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
            {
                photonView.RPC(nameof(RPC_faceOffVoter), faceOffVoters[i]);
            }
        }
    }

    public void AcroForFaceOff(AcronymSetter.acronyms acronyms)
    {
        switch (acronyms)
        {
            case AcronymSetter.acronyms.ThreeLetters:
                {
                    int[] letters = new int[3];
                    letters = returnRandomAlphabet();
                    acroText = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString();
                    Debug.Log("Face Off Acro: " + acroText);
                }
                break;
            case AcronymSetter.acronyms.FourLetters:
                {
                    int[] letters = new int[4];
                    letters = returnRandomAlphabet();

                    acroText = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString()
    + alphabets[letters[3]].ToString();
                    Debug.Log("Face Off Acro: " + acroText);
                    break;
                }
            case AcronymSetter.acronyms.FiveLetters:
                {
                    int[] letters = new int[5];
                    letters = returnRandomAlphabet();
                    acroText = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString()
    + alphabets[letters[3]].ToString() + alphabets[letters[4]].ToString();
                    Debug.Log("Face Off Acre: " + acroText);
                    break;
                }
            case AcronymSetter.acronyms.SixLetters:
                break;
            case AcronymSetter.acronyms.SevenLetters:
                break;
            default:
                break;
        }
    }


    public void updateAnswerOnPlayer(bool playerSubmitted)
    {
        RPC_UpdateAnswersForVoting(playerSubmitted);
    }
    [PunRPC]
    public void RPC_UpdateAnswerOnplayer(Player player, bool answerSubmitted)
    {
        photonView.RPC(nameof(RPC_UpdateAnswersForVoting), player, answerSubmitted);
    }
    [PunRPC]
    private void RPC_TurnOFFTextPanel()
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        //threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        votingPanel.gameObject.SetActive(true);
    }
    [PunRPC]
    private void RPC_UpdateAnswersForVoting(bool playerSubmitted)
    {
        //votingPanel.voteTimer.StartTimer();
        votingPanel.updateAnswers(playerSubmitted);
    }
    [PunRPC]
    private void RPC_ShowWelcomePanel()
    {
        welcomePanel.SetActive(true);
    }

    [PunRPC]
    private void RPC_ShowFirstRoundPanel()
    {
        if (checkIfTheVotingIsInProgress())
        {
            return;
        }
        StartLevelOfAcronym(AcronymSetter.acronyms.ThreeLetters,GameSettings.ThreeLetterRoundTime);
    }

    private void StartLevelOfAcronym(AcronymSetter.acronyms acronyms,float levelTime)
    {
        string leveltext = getlevelName(acronyms);
        roundConfigurator.setTitleText(leveltext);
        roundConfigurator.setAcronymType(acronyms);
        roundConfigurator.setTimerForRound(levelTime);
        roundConfigurator.resetAnswerField();
        threeLetterRound.SetActive(true);
        threeLetterRound.transform.GetChild(0).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(true);
    }

    public string getlevelName(AcronymSetter.acronyms acronyms)
    {
        switch (acronyms)
        {
            case AcronymSetter.acronyms.ThreeLetters:
                return "THREE LETTER ROUND";
            case AcronymSetter.acronyms.FourLetters:
                return "FOUR LETTER ROUND";
            case AcronymSetter.acronyms.FiveLetters:
                return "FIVE LETTER ROUND";
            case AcronymSetter.acronyms.SixLetters:
                return "SIX LETTER ROUND";
            case AcronymSetter.acronyms.SevenLetters:
                return "SEVEN LETTER ROUND";
            default:
                return "SEVEN LETTER ROUND";
        }
    }

    [PunRPC]
    private void RPC_ShowSecondRoundPanel()
    {
        if (checkIfTheVotingIsInProgress())
        {
            return;
        }
        StartLevelOfAcronym(AcronymSetter.acronyms.FourLetters, GameSettings.FourLetterRoundTime);
    }

    [PunRPC]
    private void RPC_ShowThirdRoundPanel()
    {
        if (checkIfTheVotingIsInProgress())
        {
            return;
        }
        StartLevelOfAcronym(AcronymSetter.acronyms.FiveLetters, GameSettings.FiveLetterRoundTime);
    }

    [PunRPC]
    private void RPC_ShowFourthRoundPanel()
    {
        if (checkIfTheVotingIsInProgress())
        {
            return;
        }
        StartLevelOfAcronym(AcronymSetter.acronyms.SixLetters, GameSettings.SixLetterRoundTime);
    }

    public bool checkIfTheVotingIsInProgress()
    {
        if (GameManager.isVotingInprogress() && (votingPanel.gameObject.activeSelf == true))
        {
            SingleWaitingPanel.SetActive(true);
            roundConfigurator.roundTimer.StartTime = false;
            roundConfigurator.roundTimer.gameObject.SetActive(false);
            return true;
        }
        else
        {
            SingleWaitingPanel.SetActive(false);
            return false;
        }
    }



    [PunRPC]
    private void RPC_ShowFifthRoundPanel()
    {
        if (checkIfTheVotingIsInProgress())
        {
            return;
        }
        StartLevelOfAcronym(AcronymSetter.acronyms.SevenLetters, GameSettings.SevenLetterRoundTime);
    }
    [PunRPC]
    private void RPC_ShowWaitingPanel()
    {
        Debug.Log("Starting face-Off round");
        waitingPanel.SetActive(true);
        waitingPanel.GetComponent<WaitingPanel>().StartGame();
    }
    [PunRPC]
    private void RPC_faceOffVoter()
    {
        Debug.Log("Starting face-Off Voter round");
        faceOffMenu.showVotersPanel();
    }
    [PunRPC]
    private void RPC_faceOffPlayer()
    {
        Debug.Log("Starting face-Off Player round");
        faceOffMenu.showPlayerPanel();
    }
    //[PunRPC]
    private void RPC_ShowFaceOffPlayerAnswer(string playerAnswer,int index)
    {
        faceOffMenu.updatePlayerAnswer(playerAnswer,index);
    }
    //[PunRPC]
    //private void RPC_ShowFaceOffP2Answer(string playerAnswer)
    //{
    //    faceOffMenu.updateP2Answer(playerAnswer);
    //}

    [PunRPC]
    private void RPC_StartFaceOffVotingTimer()
    {
        faceOffMenu.startVoteTimer();
    }

    [PunRPC]
    private void RPC_UpdateVotes()
    {
        PlayerStats.TotalVotes++;
        updateExperience();
        PlayerStatsMenu.Instance.setVotesText();
    }

    private void updateExperience()
    {
        if (GameSettings.normalGame)
        {
            Debug.Log("Add Experience for Round: " + GameManager.getroundNumber());
            switch (GameManager.getroundNumber())
            {
                case 0:
                    {
                        PlayerStats.ExperiencePoints += 3;
                        break; 
                    }
                case 1:
                    {
                        PlayerStats.ExperiencePoints += 4;
                        break;
                    }
                case 2:
                    {
                        PlayerStats.ExperiencePoints += 5;
                        break;
                    }
                case 3:
                    {
                        PlayerStats.ExperiencePoints += 6;
                        break;
                    }
                case 4:
                    {
                        PlayerStats.ExperiencePoints += 7;
                        break;
                    }
                default:
                    break;
            }
        }
        else
        {
            switch (GameManager.getFaceOffRoundNumber())
            {
                case 0:
                    PlayerStats.ExperiencePoints += 3;
                    break;
                case 1:
                    PlayerStats.ExperiencePoints += 4;
                    break;
                case 2:
                    PlayerStats.ExperiencePoints += 5;
                    break;
                default:
                    break;
            }
        }
    }

    [PunRPC]
    private void RPC_UpdateStarsUI()
    {
        PlayerStatsMenu.Instance.UpdateStarsText();
        PlayerStatsMenu.Instance.setExperienceSlider();
    }

    public void updateStars(Player targetPlayer)
    {
        photonView.RPC(nameof(RPC_UpdateVotes), targetPlayer);
    }

    public void StartVotingForOtherPlayer()
    {
        photonView.RPC(nameof(RPC_StartVotingPanel),RpcTarget.OthersBuffered);
    }


    [PunRPC]
    private void RPC_StartVotingPanel()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            RPC_UpdateAnswerOnplayer(PhotonNetwork.PlayerList[i], false);
        }
        turnOffTextPanel(true);
        GameManager.updateAnswersSubmittedNumber(4);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new NotImplementedException();
    }
}
