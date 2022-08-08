using MelonLoader;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using ABI_RC.Core;
using ABI_RC.Core.Player;
using ABI_RC.Core.Networking;
using ABI_RC.Core.Networking.IO.Social;
using System.Collections.Generic;
using DarkRift;

namespace CVRAvatarHider
{
    public class AvatarHider : MelonMod
    {

        private bool m_HideAvatars;
        private bool m_IgnoreFriends;
        //private bool m_ExcludeShownAvatars;
        //private bool m_DisableSpawnSound;

        private float m_Distance;
        //private float m_Avatars;

        public override void OnApplicationStart()
        {
            MelonPreferences.CreateCategory("CVR-AvatarHider", "CVR Avatar Hider");
            MelonPreferences.CreateEntry("CVR-AvatarHider", "HideAvatars", false, "Hide Avatars");
            MelonPreferences.CreateEntry("CVR-AvatarHider", "IgnoreFriends", true, "Ignore Friends");
            //MelonPreferences.CreateEntry("CVR-AvatarHider", "ExcludeShownAvatars", true, "Exclude Shown Avatars");
            //MelonPreferences.CreateEntry("CVR-AvatarHider", "DisableSpawnSound", false, "Disable Spawn Sounds");
            MelonPreferences.CreateEntry("CVR-AvatarHider", "HideDistance", 10.0f, "Distance (meters)");
            //MelonPreferences.CreateEntry("CVR-AvatarHider", "Avatars", 0, "Max Amount Of Shown Avatars Near Me");

            OnPreferencesSaved();

            MelonCoroutines.Start(AvatarHiderProcess());
        }

        public override void OnPreferencesSaved()
        {
            m_HideAvatars = MelonPreferences.GetEntryValue<bool>("AvatarHider", "HideAvatars");
            m_IgnoreFriends = MelonPreferences.GetEntryValue<bool>("AvatarHider", "IgnoreFriends");
            //m_ExcludeShownAvatars = MelonPreferences.GetEntryValue<bool>("AvatarHider", "ExcludeShownAvatars");
            //m_DisableSpawnSound = MelonPreferences.GetEntryValue<bool>("AvatarHider", "DisableSpawnSound");
            m_Distance = MelonPreferences.GetEntryValue<float>("AvatarHider", "HideDistance");

            UnHideAllAvatars();
        }

        //public static List<CVRPlayerEntity> networkPlayersCVR = CVRPlayerManager.Instance.NetworkPlayers;

        private IEnumerator AvatarHiderProcess()
        {
            while (true)
            {
                try
                {
                    if (m_HideAvatars)
                    {
                        //find your avatar game object and id
                        GameObject myLocalPlayer = GameObject.Find("_PLAYERLOCAL");
                        string myOwnerID = myLocalPlayer.GetComponent<PlayerDescriptor>().ownerId;
                        if (myLocalPlayer != null)
                        {
                            //check connection
                            if (NetworkManager.Instance.GameNetwork.ConnectionState == ConnectionState.Connected)
                            {
                                //check each player in instance for show/hide status
                                foreach (var player in CVRPlayerManager.Instance.NetworkPlayers)
                                {
                                    //does not affect your own avatar
                                    if (player.PlayerDescriptor.ownerId != myOwnerID)
                                    {
                                        var avatar = player.PlayerObject.transform.Find("[PlayerAvatar]");
                                        var distance = Vector3.Distance(myLocalPlayer.transform.position, player.PuppetMaster.avatarRootPosition);

                                        if (
                                        avatar != null
                                        && !(m_IgnoreFriends && Friends.FriendsWith(player.Uuid))
                                        )
                                        //&& !(m_ExcludeShownAvatars && player.IsShowingAvatar())
                                        {
                                            //hide avatars far away
                                            if (distance > m_Distance && avatar.gameObject.activeSelf)
                                            {
                                                avatar.gameObject.SetActive(false);
                                            }
                                            //show close avatar if hidden
                                            else if (distance <= m_Distance && !avatar.gameObject.activeSelf)
                                            {
                                                avatar.gameObject.SetActive(true);
                                            }
                                        } 
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MelonLogger.Msg(ConsoleColor.Red, $"Failed to unhide avatar {e}");
                }
                
                yield return new WaitForSeconds(1f);
            }
        }

        private void UnHideAllAvatars()
        {
            try
            {
                if (m_HideAvatars)
                {
                    GameObject myLocalPlayer = GameObject.Find("_PLAYERLOCAL");
                    string myOwnerID = myLocalPlayer.GetComponent<PlayerDescriptor>().ownerId;
                    if (myLocalPlayer != null)
                    {
                        if (NetworkManager.Instance.GameNetwork.ConnectionState == ConnectionState.Connected)
                        {
                            foreach (var player in CVRPlayerManager.Instance.NetworkPlayers)
                            {
                                if (player.PlayerDescriptor.ownerId != myOwnerID)
                                {
                                    var avatar = player.PlayerObject.transform.Find("[PlayerAvatar]");

                                    if (avatar != null && !avatar.gameObject.activeSelf) avatar.gameObject.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
                {
                    MelonLogger.Msg(ConsoleColor.Red, $"Failed to unhide avatar {e}");
                }

        }
    }
}