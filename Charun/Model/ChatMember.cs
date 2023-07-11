﻿namespace Charun.Model
{
    public class ChatMember
    {
        public string ProfileId { get; set; }

        public string Name { get; set; }
        public AvatarModel Avatar { get; set; }

        public bool Blocked { get; set; }
    }
}