﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using YAPO.Global;

namespace YAPO.Services {
    public static class TroopActionService {
        public static (int, int, int) UpgradeTroops(PartyVM partyVm, PartyScreenLogic partyScreenLogic) {
            List<PartyCharacterVM> upgradablePrisoners = partyVm.MainPartyTroops.Where(x => !x.IsHero && x.IsUpgrade1Available && x.NumOfTarget1UpgradesAvailable > 0 || x.IsUpgrade2Available && x.NumOfTarget2UpgradesAvailable > 0).ToList();
            if (!upgradablePrisoners.Any()) {
                Global.Helpers.Message("No troops available to upgrade");
                return (0, 0, 0);
            }

            int upgradedTotal = 0;
            int upgradedTypes = 0;
            int multiPathSkipped = 0;
            List<Tuple<PartyCharacterVM, PartyScreenLogic.PartyCommand>> commands = new List<Tuple<PartyCharacterVM, PartyScreenLogic.PartyCommand>>();
            foreach (PartyCharacterVM troops in upgradablePrisoners) {
                if (troops.IsUpgrade2Exists) {
                    multiPathSkipped++;
                    continue;
                }

                int troopsToUpgrade = troops.NumOfUpgradeableTroops;
                upgradedTypes++;
                upgradedTotal += troopsToUpgrade;

                PartyScreenLogic.PartyCommand upgradeCommand = new PartyScreenLogic.PartyCommand();
                upgradeCommand.FillForUpgradeTroop(troops.Side, troops.Type, troops.Character, troopsToUpgrade, PartyScreenLogic.PartyCommand.UpgradeTargetType.UpgradeTarget1);
                commands.Add(new Tuple<PartyCharacterVM, PartyScreenLogic.PartyCommand>(troops, upgradeCommand));
            }

            if (upgradedTotal == 0) {
                Global.Helpers.Message(multiPathSkipped > 0 ? $"No troops upgraded. {multiPathSkipped} troop types with mulit-path upgrades were skipped" : "No troops upgraded");
                return (0, 0, 0);
            }

            MethodInfo upgradeTroop = partyScreenLogic.GetType().GetMethod("UpgradeTroop", BindingFlags.NonPublic | BindingFlags.Instance);
            if (upgradeTroop == null) {
                Global.Helpers.DebugMessage("Could not find UpgradeTroop method in PartyScreenLogic");
                return (0, 0, 0);
            }

            foreach ((PartyCharacterVM partyCharacterVm, PartyScreenLogic.PartyCommand command) in commands) {
                partyVm.CurrentCharacter = partyCharacterVm;
                upgradeTroop.Invoke(partyScreenLogic, new object[] {command});
            }

            return (upgradedTotal, upgradedTypes, multiPathSkipped);
        }

        public static (int, int) RecruitPrisoners(PartyVM partyVm, PartyScreenLogic partyScreenLogic) {
            List<PartyCharacterVM> recruitablePrisoners = partyVm.MainPartyPrisoners.Where(x => !x.IsHero && x.NumOfRecruitablePrisoners > 0).ToList();
            if (!recruitablePrisoners.Any()) {
                Global.Helpers.Message("No prisoners available to recruit");
                return (0, 0);
            }
            
            int partySpace = partyScreenLogic.RightOwnerParty.PartySizeLimit - partyScreenLogic.MemberRosters[1].TotalManCount;
            if (partySpace <= 0) {
                if (States.HotkeyControl) {
                    Global.Helpers.Message("Party limit reached! Limit overriden, proceeding...");
                } else {
                    Global.Helpers.Message("Party limit reached! Cannot recruit prisoners. (Hold control to override)");
                    return (0, 0);
                }
            }

            int recruitedTypes = 0;
            int recruitedTotal = 0;
            List<Tuple<PartyCharacterVM, PartyScreenLogic.PartyCommand>> commands = new List<Tuple<PartyCharacterVM, PartyScreenLogic.PartyCommand>>();
            while (recruitablePrisoners.Count > 0 && (partySpace != 0 || States.HotkeyControl)) {
                PartyCharacterVM prisoners = recruitablePrisoners.First();
                int prisonerCount = prisoners.NumOfRecruitablePrisoners;
                int prisonersToRecruit = Math.Min(prisonerCount, partySpace);
                recruitedTypes++;
                recruitedTotal += prisonersToRecruit;
                partySpace -= prisonersToRecruit;

                PartyScreenLogic.PartyCommand recruitCommand = new PartyScreenLogic.PartyCommand();
                recruitCommand.FillForRecruitTroop(prisoners.Side, prisoners.Type, prisoners.Character, prisonersToRecruit);
                commands.Add(new Tuple<PartyCharacterVM, PartyScreenLogic.PartyCommand>(prisoners, recruitCommand));
                
                recruitablePrisoners.RemoveAt(0);
            }

            if (commands.Count == 0) {
                Global.Helpers.Message("No prisoners recruited");
                return (0, 0);
            }

            MethodInfo recruitPrisoner = partyScreenLogic.GetType().GetMethod("RecruitPrisoner", BindingFlags.NonPublic | BindingFlags.Instance);
            if (recruitPrisoner == null) {
                Global.Helpers.DebugMessage("Could not find RecruitPrisoner method in PartyScreenLogic");
                return (0, 0);
            }

            foreach ((PartyCharacterVM partyCharacterVm, PartyScreenLogic.PartyCommand command) in commands) {
                partyVm.CurrentCharacter = partyCharacterVm;
                recruitPrisoner.Invoke(partyScreenLogic, new object[] {command});
            }

            return (recruitedTotal, recruitedTypes);
        }
    }
}