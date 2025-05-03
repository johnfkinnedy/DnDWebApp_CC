using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace DnDWebApp_CC.Services
{
    
    public class DataInitializer
    {
        private readonly ApplicationDbContext _db;

        

        public DataInitializer(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets a skill from a name
        /// </summary>
        /// <param name="skillName">the name of the skill</param>
        /// <returns>a skill</returns>
        public async Task<Skill> GetSkill(string skillName)
        {
            return await _db.Skills.FirstAsync(s => s.Name == skillName);
        }

        /// <summary>
        /// Gets a spell from a name
        /// </summary>
        /// <param name="spellName">the name of the spell</param>
        /// <returns>a spell</returns>
        public async Task<Spell> GetSpell(string spellName)
        {
            return await _db.Spells.FirstAsync(s => s.Name.ToLower() == spellName.ToLower());
        }
        /// <summary>
        /// Seeds the dice
        /// </summary>
        /// <returns></returns>
        public async Task SeedDiceAsync()
        {
            _db.Database.EnsureCreated();
            if (_db.Dice.Any())
            {
                return;
            }
            var dice = new List<Dice>
            {
                new Dice("d4"), 
                new Dice("d6"),
                new Dice("d8"),
                new Dice("d10"),
                new Dice("d12"),
                new Dice("d20")
            };
            await _db.Dice.AddRangeAsync(dice);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Seeds the stats
        /// </summary>
        /// <returns></returns>
        public async Task SeedStatsAsync()
        {
            _db.Database.EnsureCreated();
            if(_db.Stats.Any())
            {
                return;
            }
            var stats = new List<Stat>
            {
                new Stat("Strength"),
                new Stat("Dexterity"),
                new Stat("Constitution"),
                new Stat("Intelligence"),
                new Stat("Wisdom"),
                new Stat("Charisma"),
            };
            await _db.Stats.AddRangeAsync(stats);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Seeds the skills
        /// </summary>
        /// <returns></returns>
        public async Task SeedSkillsAsync()
        {
            _db.Database.EnsureCreated();

            if (_db.Skills.Any())
            {
                return;
            }
            var allStats = await _db.Stats.ToListAsync();


            ICollection<Skill> skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Athletics",
                        Description = "Your Strength (Athletics) check covers difficult situations you encounter while climbing, jumping, or swimming.",
                        
                            BaseStat = allStats.First(s => s.Name == "Strength")
                        
                    },
                    new Skill
                    {
                        Name ="Acrobatics",
                        Description ="Your Dexterity (Acrobatics) check covers your attempt to stay on your feet in a tricky situation, such as when you’re trying to run across a sheet of ice, balance on a tightrope, or stay upright on a rocking ship’s deck. The GM might also call for a Dexterity (Acrobatics) check to see if you can perform acrobatic stunts, including dives, rolls, somersaults, and flips.",
                        BaseStat  = allStats.First(s => s.Name == "Dexterity")
                        
                    },
                    new Skill
                    {
                        Name ="Sleight of Hand",
                        Description ="Whenever you attempt an act of legerdemain or manual trickery, such as planting something on someone else or concealing an object on your person, make a Dexterity (Sleight of Hand) check. The GM might also call for a Dexterity (Sleight of Hand) check to determine whether you can lift a coin purse off another person or slip something out of another person’s pocket.",
                       BaseStat = allStats.First(s => s.Name == "Dexterity")
                        
                    },
                    new Skill
                    {
                       Name = "Stealth",
                        Description = "Make a Dexterity (Stealth) check when you attempt to conceal yourself from enemies, slink past guards, slip away without being noticed, or sneak up on someone without being seen or heard.",
                        BaseStat =allStats.First(s => s.Name == "Dexterity")
                        
                    },
                    new Skill
                    {
                       Name = "Arcana",
                        Description = "Your Intelligence (Arcana) check measures your ability to recall lore about spells, magic items, eldritch symbols, magical traditions, the planes of existence, and the inhabitants of those planes.",
                        BaseStat = allStats.First(s => s.Name == "Intelligence")
                        
                    },
                    new Skill
                    {
                       Name = "History",
                        Description = "Your Intelligence (History) check measures your ability to recall lore about historical events, legendary people, ancient kingdoms, past disputes, recent wars, and lost civilizations.",
                        BaseStat =  allStats.First(s => s.Name == "Intelligence")
                    },
                    new Skill
                    {
                       Name = "Investigation",
                        Description = "When you look around for clues and make deductions based on those clues, you make an Intelligence (Investigation) check. You might deduce the location of a hidden object, discern from the appearance of a wound what kind of weapon dealt it, or determine the weakest point in a tunnel that could cause it to collapse. Poring through ancient scrolls in search of a hidden fragment of knowledge might also call for an Intelligence (Investigation) check.",
                        BaseStat = allStats.First(s => s.Name == "Intelligence")
                        
                    },
                    new Skill
                    {
                       Name = "Nature",
                        Description = "Your Intelligence (Nature) check measures your ability to recall lore about terrain, plants and animals, the weather, and natural cycles.",
                        BaseStat = allStats.First(s => s.Name == "Intelligence")
                        
                    },
                    new Skill
                    {
                       Name = "Religion",
                        Description = "Your Intelligence (Religion) check measures your ability to recall lore about deities, rites and prayers, religious hierarchies, holy symbols, and the practices of secret cults.",
                        BaseStat =  allStats.First(s => s.Name == "Intelligence")
                    },
                    new Skill
                    {
                       Name = "Animal Handling",
                        Description = "When there is any question whether you can calm down a domesticated animal, keep a mount from getting spooked, or intuit an animal’s intentions, the GM might call for a Wisdom (Animal Handling) check. You also make a Wisdom (Animal Handling) check to control your mount when you attempt a risky maneuver.",
                        BaseStat = allStats.First(s => s.Name == "Wisdom")
                        
                    },
                    new Skill
                    {
                       Name = "Insight",
                        Description = "Your Wisdom (Insight) check decides whether you can determine the true intentions of a creature, such as when searching out a lie or predicting someone’s next move. Doing so involves gleaning clues from body language, speech habits, and changes in mannerisms.",
                        BaseStat = allStats.First(s => s.Name == "Wisdom")
                        
                    },
                    new Skill
                    {
                       Name = "Medicine",
                        Description = "A Wisdom (Medicine) check lets you try to stabilize a dying companion or diagnose an illness.",
                        BaseStat =  allStats.First(s => s.Name == "Wisdom")
                    },
                    new Skill
                    {
                       Name = "Perception",
                        Description = "Your Wisdom (Perception) check lets you spot, hear, or otherwise detect the presence of something. It measures your general awareness of your surroundings and the keenness of your senses. For example, you might try to hear a conversation through a closed door, eavesdrop under an open window, or hear monsters moving stealthily in the forest. Or you might try to spot things that are obscured or easy to miss, whether they are orcs lying in ambush on a road, thugs hiding in the shadows of an alley, or candlelight under a closed secret door.",
                        BaseStat =  allStats.First(s => s.Name == "Wisdom")
                    },
                    new Skill
                    {
                       Name = "Survival",
                        Description = "The GM might ask you to make a Wisdom (Survival) check to follow tracks, hunt wild game, guide your group through frozen wastelands, identify signs that owlbears live nearby, predict the weather, or avoid quicksand and other natural hazards.",
                        BaseStat =  allStats.First(s => s.Name == "Wisdom") 
                    },
                    new Skill
                    {
                       Name = "Deception",
                        Description = "Your Charisma (Deception) check determines whether you can convincingly hide the truth, either verbally or through your actions. This deception can encompass everything from misleading others through ambiguity to telling outright lies. Typical situations include trying to fast- talk a guard, con a merchant, earn money through gambling, pass yourself off in a disguise, dull someone’s suspicions with false assurances, or maintain a straight face while telling a blatant lie.",
                        BaseStat =  allStats.First(s => s.Name == "Charisma") 
                    },
                    new Skill
                    {
                       Name = "Intimidation",
                        Description = "When you attempt to influence someone through overt threats, hostile actions, and physical violence, the GM might ask you to make a Charisma (Intimidation) check. Examples include trying to pry information out of a prisoner, convincing street thugs to back down from a confrontation, or using the edge of a broken bottle to convince a sneering vizier to reconsider a decision.",
                        BaseStat =  allStats.First(s => s.Name == "Charisma") 
                    },
                    new Skill
                    {
                       Name = "Performance",
                        Description = "Your Charisma (Performance) check determines how well you can delight an audience with music, dance, acting, storytelling, or some other form of entertainment.",
                        BaseStat =  allStats.First(s => s.Name == "Charisma") 
                    } ,
                    new Skill
                    {
                       Name = "Persuasion",
                        Description = "When you attempt to influence someone or a group of people with tact, social graces, or good nature, the GM might ask you to make a Charisma (Persuasion) check. Typically, you use persuasion when acting in good faith, to foster friendships, make cordial requests, or exhibit proper etiquette. Examples of persuading others include convincing a chamberlain to let your party see the king, negotiating peace between warring tribes, or inspiring a crowd of townsfolk.",
                        BaseStat =  allStats.First(s => s.Name == "Charisma") 
                    }


                };
            
            await _db.Skills.AddRangeAsync(skills);
            await _db.SaveChangesAsync();
        }
        

        /// <summary>
        /// Seeds spells
        /// </summary>
        /// <returns></returns>
        public async Task SeedSpellsAsync()
        {
            _db.Database.EnsureCreated();

            if (_db.Spells.Any())
            {
                return;
            }

            var allDice = await _db.Dice.ToListAsync();

            var spells = new List<Spell>
            {
                new Spell
                {
                    Name = "Healing Word",
                    Description = "You speak words of healing and recover your allies hit points by the specified dice",
                    DiceDenomination = allDice.Find(d => d.Size == "d6"),
                    DiceToRoll = 2,
                    SlotLevel = 1

                },
                new Spell
                {
                    Name = "Fireball",
                    Description = "A ball of flames.",
                    DiceDenomination = allDice.Find(d => d.Size == "d8"),
                    SlotLevel = 1,
                    DiceToRoll = 3
                },
                new Spell
                {
                    Name = "Eldritch Blast",
                    Description = "A crackling blast of energy, enriched with magic that is not yours to control.",
                    DiceDenomination = allDice.Find(d => d.Size == "d10"),
                    SlotLevel = 0,
                    DiceToRoll = 1
                },
                new Spell
                {
                    Name = "Fire bolt",
                    Description = "A small bolt of fire to smite your enemies",
                    DiceDenomination = allDice.Find(d => d.Size == "d8"),
                    DiceToRoll = 1,
                    SlotLevel = 0
                },
                new Spell
                {
                    Name = "Arcane Lock",
                    Description = "You touch a closed door, window, gate, chest, or other entryway, and it becomes locked for the duration.",
                    SlotLevel = 2,                    
                },
                new Spell
                {
                    Name = "Barkskin",
                    Description = "You touch a willing creature. Until the spell ends, the target’s skin has a rough, bark-like appearance, and the target’s AC can’t be less than 16, regardless of what kind of armor it is wearing.",
                    SlotLevel = 2
                },
                new Spell
                {
                    Name = "Cloud of Daggers",
                    Description = "You fill the air with spinning daggers in a cube 5 feet on each side, centered on a point you choose within range.",
                    DiceDenomination = allDice.Find(d => d.Size == "d4"),
                    DiceToRoll = 4,
                    SlotLevel = 0
                },
                new Spell
                {
                    Name = "Call Lightning",
                    Description = "You create a stormcloud, and call down a lightning strike upon your foes",
                    DiceDenomination = allDice.Find(d => d.Size == "d10 "),
                    DiceToRoll = 3,
                    SlotLevel = 0
                },
                new Spell
                {
                    Name = "Revivify",
                    Description = "You touch a creature that has died within the last minute. That creature returns to life with 1 hit point. This spell can’t return to life a creature that has died of old age, nor can it restore any missing body parts.",
                    SlotLevel = 3
                },
                new Spell
                {
                    Name = "Armor of Agathys",
                    Description = "A protective magical force surrounds you, manifesting as a spectral frost that covers you and your gear. You gain 5 temporary hit points for the duration. If a creature hits you with a melee attack while you have these hit points, the creature takes 5 cold damage.",
                    SlotLevel = 0
                },
            };
            await _db.Spells.AddRangeAsync(spells);
            await _db.SaveChangesAsync();
        }
        
        /// <summary>
        /// Seeds backgrounds
        /// </summary>
        /// <returns></returns>
        public async Task SeedBackgroundsAsync()
        {
            _db.Database.EnsureCreated();
            if (_db.Backgrounds.Any())
            {
                return;
            }
            var allSkills = await _db.Skills.ToListAsync();

            ICollection<Background> backgrounds = new List<Background>
            {
                new Background
                { // insight, religion
                    Name = "Acolyte",
                    Description = "You have spent your life in the service of a temple to a specific god or pantheon of gods. You act as an intermediary between the realm of the holy and the mortal world, performing sacred rites and offering sacrifices in order to conduct worshipers into the presence of the divine. You are not necessarily a cleric — performing sacred rites is not the same thing as channeling divine power.",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {
                            
                            Skill = await GetSkill("Insight")
                        }, 
                        new SkillInBackground
                        {
                           Skill =  await GetSkill("Religion")
                        }
                    },
                    Languages = new List<string>
                    {
                        "Two of your choice"
                    },
                    Features = new List<string>
                    {
                        "Shelter of the Faithful: As an acolyte, you command the respect of those who share your faith, and you can perform the religious ceremonies of your deity. You and your adventuring companions can expect to receive free healing and care at a temple, shrine, or other established presence of your faith, though you must provide any material components needed for spells. Those who share your religion will support you (but only you) at a modest lifestyle.\r \r \r You might also have ties to a specific temple dedicated to your chosen deity or pantheon, and you have a residence there. This could be the temple where you used to serve, if you remain on good terms with it, or a temple where you have found a new home. While near your temple, you can call upon the priests for assistance, provided the assistance you ask for is not hazardous and you remain in good standing with your temple. "
                    }
                },
                new Background
                { // deception, sleight of hand
                    Name = "Charlatan",
                    Description = "You have always had a way with people. You know what makes them tick, you can tease out their hearts' desires after a few minutes of conversation, and with a few leading questions you can read them like they were a children's book. It's a useful talent, and one that you're perfectly willing to use for your advantage. ",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Deception")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Sleight of Hand")
                        }
                    },
                    Languages = new List<string>
                    {

                    },
                    Features = new List<string>
                    {
                        "False Identity: You have created a second identity that includes documentation, established acquaintances, and disguises that allow you to assume that persona. Additionally, you can forge documents including official papers and personal letters, as long as you have seen an example of the kind of document or the handwriting you are trying to copy. "
                    }
                },
                new Background
                { // deception, stealth
                    Name = "Criminal",
                    Description = "You are an experienced criminal with a history of breaking the law. You have spent a lot of time among other criminals and still have contacts within the criminal underworld. You're far closer than most people to the world of murder, theft, and violence that pervades the underbelly of civilization, and you have survived up to this point by flouting the rules and regulations of society.",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Deception")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Stealth")
                        }
                    },
                    Languages = new List<string>
                    {

                    },
                    Features = new List <string>
                    {
                        "Criminal Contact: You are an experienced criminal with a history of breaking the law. You have spent a lot of time among other criminals and still have contacts within the criminal underworld. You're far closer than most people to the world of murder, theft, and violence that pervades the underbelly of civilization, and you have survived up to this point by flouting the rules and regulations of society."
                    }
                },
                new Background
                { // animal handling, survival
                    Name = "Folk Hero",
                    Description = "You come from a humble social rank, but you are destined for so much more. Already the people of your home village regard you as their champion, and your destiny calls you to stand against the tyrants and monsters that threaten the common folk everywhere.",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Animal Handling")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Survival")
                        }
                    },
                    Languages = new List<string>
                    {

                    },
                    Features = new List<string>
                    {
                        "Rustic Hospitality: Since you come from the ranks of the common folk, you fit in among them with ease. You can find a place to hide, rest, or recuperate among other commoners, unless you have shown yourself to be a danger to them. They will shield you from the law or anyone else searching for you, though they will not risk their lives for you."
                    }
                },
                new Background
                { // insight, persuasion
                    Name = "Guild Artisan",
                    Description = "You are a member of an artisan's guild, skilled in a particular field and closely associated with other artisans. You are a well-established part of the mercantile world, freed by talent and wealth from the constraints of a feudal social order. You learned your skills as an apprentice to a master artisan, under the sponsorship of your guild, until you became a master in your own right. ",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Insight")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Persuasion")
                        }
                    },
                    Languages = new List<string>
                    {
                        "One of your choice"
                    },
                    Features = new List<string>
                    {
                        "Guild Membership: As an established and respected member of a guild, you can rely on certain benefits that membership provides. Your fellow guild members will provide you with lodging and food if necessary, and pay for your funeral if needed. In some cities and towns, a guildhall offers a central place to meet other members of your profession, which can be a good place to meet potential patrons, allies, or hirelings.\r\n\r\nGuilds often wield tremendous political power. If you are accused of a crime, your guild will support you if a good case can be made for your innocence or the crime is justifiable. You can also gain access to powerful political figures through the guild, if you are a member in good standing. Such connections might require the donation of money or magic items to the guild's coffers.\r\n\r\nYou must pay dues of 5 gp per month to the guild. If you miss payments, you must make up back dues to remain in the guild's good graces."
                    }
                },
                new Background
                { // medicine, religion
                    Name = "Hermit",
                    Description = "You lived in seclusion — either in a sheltered community such as a monastery, or entirely alone — for a formative part of your life. In your time apart from the clamor of society, you found quiet, solitude, and perhaps some of the answers you were looking for. ",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Medicine")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Religion")
                        }
                    },
                    Languages = new List<string>
                    {
                        "One of your choice"
                    },
                    Features = new List<string>
                    {
                        "Discovery: The quiet seclusion of your extended hermitage gave you access to a unique and powerful discovery. The exact nature of this revelation depends on the nature of your seclusion. It might be a great truth about the cosmos, the deities, the powerful beings of the outer planes, or the forces of nature. It could be a site that no one else has ever seen. You might have uncovered a fact that has long been forgotten, or unearthed some relic of the past that could rewrite history. It might be information that would be damaging to the people who or consigned you to exile, and hence the reason for your return to society.\r\n\r\nWork with your DM to determine the details of your discovery and its impact on the campaign. "
                    }
                },
                new Background
                { // history, persuasion
                    Name = "Noble",
                    Description = "You understand wealth, power, and privilege. You carry a noble title, and your family owns land, collects taxes, and wields significant political influence. You might be a pampered aristocrat unfamiliar with work or discomfort, a former merchant just elevated to the nobility, or a disinherited scoundrel with a disproportionate sense of entitlement. Or you could be an honest, hard-working landowner who cares deeply about the people who live and work on your land, keenly aware of your responsibility to them. ",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("History")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Persuasion")
                        }
                    },
                    Languages = new List<string>
                    {
                        "One of your choice"
                    },
                    Features = new List<string>
                    {
                        "Position of Privilege: Thanks to your noble birth, people are inclined to think the best of you. You are welcome in high society, and people assume you have the right to be wherever you are. The common folk make every effort to accommodate you and avoid your displeasure, and other people of high birth treat you as a member of the same social sphere. You can secure an audience with a local noble if you need to. "
                    }
                },
                new Background
                { // athletics, survival
                    Name = "Outlander",
                    Description = "You grew up in the wilds, far from civilization and the comforts of town and technology. You've witnessed the migration of herds larger than forests, survived weather more extreme than any city-dweller could comprehend, and enjoyed the solitude of being the only thinking creature for miles in any direction. The wilds are in your blood, whether you were a nomad, an explorer, a recluse, a hunter-gatherer, or even a marauder. Even in places where you don't know the specific features of the terrain, you know the ways of the wild.",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Athletics")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Survival")
                        }
                    },
                    Languages = new List<string>
                    {
                        "One of your choice"
                    },
                    Features = new List<string>
                    {
                        "Wanderer: You have an excellent memory for maps and geography, and you can always recall the general layout of terrain, settlements, and other features around you. In addition, you can find food and fresh water for yourself and up to five other people each day, provided that the land offers berries, small game, water, and so forth."
                    }
                },
                new Background
                { // arcana, history
                    Name = "Sage",
                    Description = "You spent years learning the lore of the multiverse. You scoured manuscripts, studied scrolls, and Listened to the greatest experts on the subjects that interest you. Your efforts have made you a master in your fields of study. ",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Arcana")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("History")
                        }
                    },
                    Languages = new List<string>
                    {
                        "Two of your choice"
                    },
                    Features = new List<string>
                    {
                        "Researcher: When you attempt to learn or recall a piece of lore, if you do not know that information, you often know where and from whom you can obtain it. Usually, this information comes from a library, scriptorium, university, or a sage or other learned person or creature, Your DM might rule that the knowledge you seek is secreted away in an almost inaccessible place, or that it simply cannot be found. Unearthing the deepest secrets of the multiverse can require an adventure or even a whole campaign."
                    }
                },
                new Background
                { // athletics, perception
                    Name = "Sailor",
                    Description = "You sailed on a seagoing vessel for years. In that time, you faced down mighty storms, monsters of the deep, and those who wanted to sink your craft to the bottomless depths. Your first love is the distant line of the horizon, but the time has come to try your hand at something new. ",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Athletics")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Perception")
                        }
                    },
                    Languages = new List<string>
                    {

                    },
                    Features = new List<string>
                    {
                        "Ship's Passage: When you need to, you can secure free passage on a sailing ship for yourself and your adventuring companions. You might sail on the ship you served on, or another ship you have good relations with (perhaps one captained by a former crewmate). Because you're calling in a favor, you can't be certain of a schedule or route that will meet your every need. Your Dungeon Master will determine how long it takes to get where you need to go. In return for your free passage, you and your companions are expected to assist the crew during the voyage."
                    }
                },
                new Background
                { // athletics, intimidation
                    Name = "Soldier",
                    Description = "War has been your life for as long as you care to remember. You trained as a youth, studied the use of weapons and armor, learned basic survival techniques, including how to stay alive on the battlefield. You might have been part of a standing national army or a mercenary company, or perhaps a member of a local militia who rose to prominence during a recent war.",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Athletics")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Intimidation")
                        }
                    },
                    Languages = new List<string>
                    {

                    },
                    Features = new List<string>
                    {
                        "Military Rank: You have a military rank from your career as a soldier. Soldiers loyal to your former military organization still recognize your authority and influence, and they defer to you if they are of a lower rank. You can invoke your rank to exert influence over other soldiers and requisition simple equipment or horses for temporary use. You can also usually gain access to friendly military encampments and fortresses where your rank is recognized."
                    }
                },
                new Background
                { // sleight of hand, stealth
                    Name = "Urchin",
                    Description = "You grew up on the streets alone, orphaned, and poor. You had no one to watch over you or to provide for you, so you learned to provide for yourself. You fought fiercely over food and kept a constant watch out for other desperate souls who might steal from you. You slept on rooftops and in alleyways, exposed to the elements, and endured sickness without the advantage of medicine or a place to recuperate. You've survived despite all odds, and did so through cunning, strength, speed, or some combination of each. ",
                    Skills = new List<SkillInBackground>
                    {
                        new SkillInBackground
                        {

                            Skill = await GetSkill("Sleight of Hand")
                        },
                        new SkillInBackground
                        {
                            Skill = await GetSkill("Stealth")
                        }
                    },
                    Languages = new List<string>
                    {

                    },
                    Features = new List<string>
                    {
                        "City Secrets: You know the secret patterns and flow to cities and can find passages through the urban sprawl that others would miss. When you are not in combat, you (and companions you lead) can travel between any two locations in the city twice as fast as your speed would normally allow."
                    }
                }
            };
            await _db.Backgrounds.AddRangeAsync(backgrounds);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Seeds species
        /// </summary>
        /// <returns></returns>
        public async Task SeedSpeciesAsync()
        {
            _db.Database.EnsureCreated();
            if (_db.Species.Any())
            {
                return;
            }

            var allSkills = await _db.Skills.ToListAsync();

            var species = new List<Species>
            {
                new Species
                {
                    Name = "Human",
                    Description = "Featherless Biped",
                    Languages = new List<string>{ "Common", "Undercommon"},
                    Skills = new List<SkillsInSpecies>{ new SkillsInSpecies { Skill = await GetSkill("Athletics") }, new SkillsInSpecies { Skill = await GetSkill("Stealth") } }

                },
                new Species
                {
                    Name = "Elf",
                    Description = "Featherless Longer Biped",
                    Languages = new List<string>{ "Common", "Elvish"},
                    Skills = new List<SkillsInSpecies>{ new SkillsInSpecies { Skill = await GetSkill("Deception") }, new SkillsInSpecies { Skill = await GetSkill("Persuasion") } }

                }
            };
            await _db.Species.AddRangeAsync(species);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Seeds classes
        /// </summary>
        /// <returns></returns>
        public async Task SeedClassesAsync()
        {
            _db.Database.EnsureCreated();

            if (_db.CharacterClasses.Any())
            {
                return;
            }

            var allSkills = await _db.Skills.ToListAsync();
            var allDice = await _db.Dice.ToListAsync();
            var allSpells = await _db.Spells.ToListAsync();
            Console.WriteLine(allSpells.Count);
            //name, description, hitdice, skills, features
            // spellcaster bool, spells?
            var classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Fighter",
                    Description = "A master of all arms and armor",
                    HitDice = allDice.First(d => d.Size == "d10"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Acrobatics")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Athletics")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Intimidation")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Persuasion")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Perception")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Survival")
                        }
                    },
                    Features = new List<String>{"Fighting Style: You gain a fighting style of your choice", "Second wind: As a bonus action, you can regain hit points equal to 1d10 + your fighter level"},
                    Proficiencies = new List<String>{"Light, medium, and heavy armor", "Simple and Martial weapons",},
                    Spellcaster = false
                },
                new CharacterClass
                {
                    Name = "Monk",
                    Description = "A martial artist of supernatural focus",
                    HitDice = allDice.First(d => d.Size == "d8"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Acrobatics")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Religion")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("History")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Stealth")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        }
                    },
                    Features = new List<String>{"Martial Arts", "Unarmored Defense"},
                    Proficiencies = new List<String>{"Simple weapons, martial weapons with the Light property", "One type of artisan's tools or musical instrument"},
                    Spellcaster = false
                },
                new CharacterClass
                {
                    Name = "Barbarian",
                    Description = "A Fierce Warrior of Primal Rage",
                    HitDice = allDice.First(d => d.Size == "d12"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Athletics")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Intimidation")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Nature")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Perception")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Survival")
                        }
                    },
                    Features = new List<String>{"Rage", "Unarmored Defense"},
                    Proficiencies = new List<String>{"Simple weapons, martial weapons", "Light and Medium armor", "Shields"},
                    Spellcaster = false
                },
                new CharacterClass
                {
                    Name = "Rogue",
                    Description = "A Dexterous Expert in Stealth and Subterfuge",
                    HitDice = allDice.First(d => d.Size == "d8"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Acrobatics")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Athletics")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Deception")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Stealth")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        }
                    },
                    Features = new List<String>{"Expertise", "Sneak Attack"},
                    Proficiencies = new List<String>{"Simple weapons, martial weapons with the Light or Finesse property", "Thieve's tools", "Light armor"},
                    Spellcaster = false
                },
                new CharacterClass
                {
                    Name = "Cleric",
                    Description = "heals stuff",
                    HitDice = allDice.First(d => d.Size == "d6"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Religion")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Perception")
                        }
                    },
                    Features = new List<String>{"Feature one", "Feature two"},
                    Proficiencies = new List<String>{"good at healing", "not much else"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Healing Word")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Fireball")
                        }
                    }
                },
                new CharacterClass
                {
                    Name = "Bard",
                    Description = "An Inspiring Performer of Music, Dance, and Magic",
                    HitDice = allDice.First(d => d.Size == "d8"),
                    Skills = new List<ClassSkills>(),
                    Features = new List<String>{"Bardic Inspiration", "Spellcasting"},
                    Proficiencies = new List<String>{"Simple weapons","Light Armor", "Choose 3 Musical Instrumens"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Healing Word")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Arcane Lock")
                        }
                    }
                },
                new CharacterClass
                {
                    Name = "Druid",
                    Description = "A nature priest of primal power",
                    HitDice = allDice.First(d => d.Size == "d8"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Animal Handling")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Arcana")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Medicine")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Nature")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Religion")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Perception")
                        }
                    },
                    Features = new List<String>{"Wild Resurgence"},
                    Proficiencies = new List<String>{"good at healing", "not much else"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Call Lightning")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Barkskin")
                        }
                    }
                },
                new CharacterClass
                {
                    Name = "Paladin",
                    Description = "A devout warrior of sacred oaths",
                    HitDice = allDice.First(d => d.Size == "d10"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Athletics")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Religion")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Medicine")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Intimidation")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Persuasion")
                        }
                    },
                    Features = new List<String>{"Spellcasting", "Weapon Mastery"},
                    Proficiencies = new List<String>{"Simple and Martial weapons", "Light, medium, heavy armor", "Shields"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Healing Word")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Fire Bolt")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Fireball")
                        }
                    }
                },
                new CharacterClass
                {
                    Name = "Ranger",
                    Description = "A Wandering Warrior Imbued with Primal Magic",
                    HitDice = allDice.First(d => d.Size == "d10"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Animal Handling")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Investigation")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Nature")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Survival")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Perception")
                        }
                    },
                    Features = new List<String>{"Feature one", "Feature two"},
                    Proficiencies = new List<String>{"Simple and Martial weapons", "Light and Medium armor", "Shields"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Barkskin")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Cloud of Daggers")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Call Lightning")
                        }
                    }
                },
                new CharacterClass
                {
                    Name = "Sorcerer",
                    Description = "A Dazzling Mage Filled with Innate Magic",
                    HitDice = allDice.First(d => d.Size == "d6"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Arcana")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Deception")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Intimidation")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Persuasion")
                        }
                    },
                    Features = new List<String>{"Spellcasting", "Innate Sorcery"},
                    Proficiencies = new List<String>{"Simple Weapons"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Call Lightning")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Fireball")
                        }
                    }
                },
                new CharacterClass
                {
                    Name = "Warlock",
                    Description = "A warrior that draws power from a pact with an unknown entity",
                    HitDice = allDice.First(d => d.Size == "d8"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Religion")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Arcana")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("History")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Investigation")
                        }
                    },
                    Features = new List<String>{"Eldritch Invocations", "Pact Magic"},
                    Proficiencies = new List<String>{"Simple Weapons", "Light armor"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Eldritch Blast")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Armor of Agathys")
                        }
                    }
                },
                new CharacterClass
                {
                    Name = "Wizard",
                    Description = "A Scholarly Magic-User of Arcane Power",
                    HitDice = allDice.First(d => d.Size == "d6"),
                    Skills = new List<ClassSkills>
                    {
                        new ClassSkills
                        {
                            Skill = await GetSkill("Arcana")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Insight")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Investigation")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("History")
                        },
                        new ClassSkills
                        {
                            Skill = await GetSkill("Medicine")
                        }
                    },
                    Features = new List<String>{"Spellcasting", "Ritual Adept", "Arcane Recovery"},
                    Proficiencies = new List<String>{"Simple Weapons"},
                    Spellcaster = true,
                    Spells = new List<ClassSpell>
                    {
                        new ClassSpell
                        {
                            Spell = await GetSpell("Arcane Lock")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Cloud of Daggers")
                        },
                        new ClassSpell
                        {
                            Spell = await GetSpell("Fireball")
                        }
                    }
                }
            };
            await _db.CharacterClasses.AddRangeAsync(classes);
            await _db.SaveChangesAsync();
        }
    }
}
