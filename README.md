# VRC Mods<!-- omit in toc -->

[![Tested on 1149](https://img.shields.io/badge/Build-1149-brightgreen?style=flat&logo=steam)](https://store.steampowered.com/app/438100/VRChat/)
[![GPL-3](https://img.shields.io/badge/license-GPL--3-black?style=flat&logo=open-source-initiative)](https://tldrlegal.com/license/gnu-general-public-license-v3-(gpl-3))
[![Lines of code](https://img.shields.io/tokei/lines/git.ljoonal.xyz/ljoonal/VRC-Mods?label=lines&style=flat&logo=C-Sharp)](https://vrc.ljoonal.xyz)

This repository contains my mods for [VRChat](https://store.steampowered.com/app/438100/VRChat/), using [MelonLoader](https://github.com/LavaGang/MelonLoader).

I try to minimize the things that a single mod of mine does.
That way my mods hopefully don't need that many updates, and maintenance of them will be easy.
Because I value stability over adding many fancy new features.
So a mod not receiving updates can just mean that it's still working just fine as intended.

To install, just follow the [MelonLoader wiki's instructions](https://melonwiki.xyz/#/README).
After that just drag'n'drop the DLL's into the `Mods` folder.
If you need help join the [VRChat modding group][VRCMG] to see the guide if you've not done that already.

## Warning<!-- omit in toc -->

Modding is against VRC TOS, and the devs are known to ban users who get caught using mods.
No warranty is provided for these mods, and they're provided as-is.

Please have a look at the source code & build from source for maximal safety.
I also recommend mirroring this git repository if you want to make sure you always have access to the source code even if I need to take down the public git repositories.

If you care for your safety but aren't willing to read the source code and compile the mods yourself, I'd recommend that you only grab mods that the [VRChat modding group][VRCMG] endorses.
This repository might contain some mods that they do not endorse, as they might fall into a grey area of what the moderators there want to allow.

### Using mods anyway<!-- omit in toc -->

I've yet to be banned for using mods, but I've tried to be smart about it, and suggest that you do so too:

- Don't be malicious.
- Avoid sharing evidence that you're using mods (screenshots/recordings/streaming).
- Hide that you're using mods when not in an instance exclusively made out of your friends.

It might only take a single report to get you banned if you're being naughty, but behave and you'll probably be fine.

## Mod list<!-- omit in toc -->

If you want the feature enough to get the mod for it, you should probably also go upvote the canny ticket if there is one.

- [Trust Color Changer](#trust-color-changer)
- [No Details for Clienters](#no-details-for-clienters)

### Trust Color Changer

[![Canny][CannyBadge]](https://feedback.vrchat.com/feature-requests/p/custom-colors)

A mod to allow customizing the trust colors.

![Example image](res/TrustColorChanger.jpg)

Currently implemented:

- [x] Trust Color Value
- [ ] Trust Color in Toggles
- [ ] Turst Color in Safety Settings page

Credit to emm for being the first one to do this afaik.

Also keep your eyes open for [Styletor](https://github.com/knah/VRCMods#Styletor).
Since it might eventually contain something that you can use to do the same with.

### No Details for Clienters

[![Canny][CannyBadge]](https://feedback.vrchat.com/bug-reports/p/security-users-of-modified-clients-can-see-my-ping-and-fps)

Spoof values like FPS and Ping, since VRChat shows them to remote users.

They're usually hidden behind complex debugging menus in the vanilla game.
So well hidden so well in fact that I haven't confirmed wherever they are findable without mods, but I trust the people who've told me they are.

Many clients conveniently show them though.
And often users users of such clients comment on other peoples' FPS/ping, usually with bad intent.
This mod is meant to take combat such users by rendering their bragging/spying meaningless with spoofed values that might seem realistic at first glance.

This mod is not meant to help you be "cool" or try to deceive the VRC system.
Because if the VRC devs wanted to, they could most likely easily detect the spoofing.
So I'd kindly ask that if you're going to be using this, use at least somewhat realistic values to not give VRC devs a reason to start cracking down on it and ruin it for those of us who just want some more privacy.

You might also want to check out knah's [HWID patch](https://github.com/knah/ML-UniversalMods#hwidpatch) & [NoSteamAtAll](https://github.com/knah/ML-UniversalMods#nosteamatall) if you're trying to remain a bit more private.

Credit to null from the VRC modding group discord for mentioning the values to use for spoofing.

As an additional warning, the reason this mod is not verified in the modding group is as follows:

> Ping/FPS spoofing is known to break IK interpolation, is easily detectable by VRC (if they wanted to), and those numbers are available in vanilla client via debug overlays. Lots of downsides for vanishingly little benefit

## For developers<!-- omit in toc -->

### Building<!-- omit in toc -->

Ensure that the required DLL's (listed in the `Directory.build.props` file and in the individual `.csproj` files) can be found from standard installation paths (check `Directory.build.props`).
Then use the `dotnet build` command to build.

Alternatively you can try to open the folder in Visual Studio, but I cannot provide help for using that.
If you do want to improve the situation, do feel free to contribute!

### Contacting & contributing<!-- omit in toc -->

Contact me [on the modding group Discord][VRCMG], [elsewhere](https://ljoonal.xyz/contact), and/or possibly send me git patches if you've already written any code that you'd like to get merged.

[VRCMG]: https://discord.gg/7EQCmgrUnH
[CannyBadge]: https://img.shields.io/badge/canny-ticket-pink?style=flat&logo=trello
