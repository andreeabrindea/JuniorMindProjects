using System;
namespace FootballRanking;

public class Match
{
    private readonly int _id;
    private readonly Team _homeTeam;
    private readonly int _homeTeamGoals;
    private readonly Team _awayTeam;
    private readonly int _awayTeamGoals;

    public Match(int id, Team homeTeam, int homeTeamGoals, Team awayTeam, int awayTeamGoals)
    {
        this._id = id;
        this._homeTeam = homeTeam;
        this._homeTeamGoals = homeTeamGoals;
        this._awayTeam = awayTeam;
        this._awayTeamGoals = awayTeamGoals;
    }

    public int Id => this._id;
    
    public Team HomeTeam => this._homeTeam;

    public int HomeTeamGoals => this._homeTeamGoals;

    public Team AwayTeam => this._awayTeam;

    public int AwayTeamGoals => this._awayTeamGoals;
}