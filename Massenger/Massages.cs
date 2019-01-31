﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class Messages
    {
        [Key]
        public int Id				{ get; set; }
        public int UserId			{ get; set; }
        public int RecepientId		{ get; set; }
		public DateTime DateOfSend	{ get; set; } 
        public DateTime TimeOfSend  { get; set; }
        public string TextMessage   { get; set; }

        [ForeignKey("UserId")]
        public Users User           { get; set; }
        [ForeignKey("RecepientId")]
        public Recepients Recepient { get; set; }
    }
}