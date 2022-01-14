using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.DTOs;
public class WalletCoinSimpleDto : WalletCoinEmptyDto
{
    public MemberDto appUser { get; set; }
}