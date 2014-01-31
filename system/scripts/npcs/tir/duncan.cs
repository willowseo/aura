//--- Aura Script -----------------------------------------------------------
// Duncan in Tir Chonaill
//--- Description -----------------------------------------------------------
// Good ol' Duncan
//---------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aura.Channel.Scripting;
using Aura.Channel.Scripting.Scripts;
using Aura.Channel.World.Entities;
using Aura.Shared.Mabi.Const;

public class DuncanBaseScript : NpcScript
{
	public override void Load()
	{
		SetName("_duncan");
		SetRace(10002);
		SetBody(height: 1.3f);
		SetFace(skinColor: 20, eyeType: 17);
		SetStand("human/male/anim/male_natural_stand_npc_duncan_new");
		SetLocation(1, 15409, 38310, 122);

		EquipItem(Pocket.Face, 4950, 0x93005C);
		EquipItem(Pocket.Hair, 4083, 0xBAAD9A);
		EquipItem(Pocket.Armor, 15004, 0x5E3E48, 0xD4975C, 0x3D3645);
		EquipItem(Pocket.Shoe, 17021, 0xCBBBAD);

		AddPhrase("Ah, that bird in the tree is still sleeping.");
		AddPhrase("Ah, who knows how many days are left in these old bones?");
		AddPhrase("Everything appears to be fine, but something feels off.");
		AddPhrase("Hmm....");
		AddPhrase("It's quite warm today.");
		AddPhrase("Sometimes, my memories sneak up on me and steal my breath away.");
		AddPhrase("That tree has been there for quite a long time, now that I think about it.");
		AddPhrase("The graveyard has been left unattended far too long.");
		AddPhrase("Watch your language.");
	}

	public override IEnumerable Talk(Creature c)
	{
		Bgm(c, "NPC_Duncan.mp3");
		
		Intro(c,
			"An elderly man gazes softly at the world around him with a calm air of confidence.",
			"Although his face appears weather-beaten, and his hair and beard are gray, his large beaming eyes make him look youthful somehow.",
			"As he speaks, his voice resonates with a kind of gentle authority."
		);
		
		//Hook(c, "after_intro");
		
		Msg(c, "Please let me know if you need anything.", Button("Start Conversation", "@talk"), Button("Shop", "@shop"), Button("Retrive Lost Items", "@lostandfound"));
		var selected = Select(c);
		
		switch(selected)
		{
			case "@talk":
				Msg(c, "What did you say your name was?<br/>Anyway, welcome.");
				
				while(true)
				{
					Msg(c, Hide.Name, "(Duncan is waiting for me to say something.)");
					ShowKeywords(c);
					var keyword = Select(c);
					
					//Hook(c, "keywords", keyword);
					
					switch (keyword)
					{
						case "personal_info": Msg(c, "I'm the chief of this town..."); break;
						case "rumor":         Msg(c, "I heard a rumor that this is just a copy of the world of Erin. Trippy, huh?"); break;
						case "about_skill":   Msg(c, "I don't know of any skills... Why don't you ask Malcom?"); break;
						case "about_arbeit":  Msg(c, "I don't have any jobs for you, but you can get a part time job in town."); break;
						case "about_study":   Msg(c, "You can study different magic down at the school!"); break;
						default:              Msg(c, "I don't know anything about that..."); break;
					}
				}
				break;
				
			case "@shop":
				Msg(c, "Choose a quest you would like to do.");
				OpenShop(c);
				Return();
				
			case "@lostandfound":
				Msg(c, "If you are knocked unconcious in a dungeon or field, any item you've dropped will be lost unless you get resurrected right at the spot.<br/>Lost items can usually be recovered from a Town Office or a Lost-and-Found.");
				Msg(c, "Unfortunatly, Tir Chonaill does not have a Town Office, so I run the Lost-and-Found myself.<br/>The lost items are recovered with magic,<br/>so unless you've dropped them on purpose, you can recover those items with their blessings intact.<br/>You will, however, need to pay a fee.");
				Msg(c, "As you can see, I have limited space in my home. So I can only keep 20 items for you.<br/>If there are more than 20 lost items, I'll have to throw out the oldest items to make room.<br/>I strongly suggest you retrieve any lost items you don't want to lose as soon as possible.");
				Return();
			
			default:
				Msg(c, "...");
				Return();
		}
	}
}